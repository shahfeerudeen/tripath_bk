using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;
using tripath.MiddleWare;
using tripath.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositoryRegistration(builder.Configuration);

// builder.WebHost.ConfigureKestrel(
//     (context, options) =>
//     {
//         options.Configure(context.Configuration.GetSection("Kestrel"));
//     }
// );

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MongoDb");

//If you want to swtch to MongoDB Atlas Change the name in connection string
//var connectionString = configuration.GetConnectionString("LiveMongoDb");

var databaseName = configuration.GetSection("MongoDb")["DatabaseName"];

var logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");
if (!Directory.Exists(logFolder))
    Directory.CreateDirectory(logFolder);

var logFiles = Directory.GetFiles(logFolder, "log-*.txt", SearchOption.TopDirectoryOnly);
foreach (var file in logFiles)
{
    try
    {
        var lastModified = File.GetLastWriteTime(file);
        if (lastModified < DateTime.Now.AddDays(-7))
        {
            File.Delete(file);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting old log file '{file}': {ex.Message}");
    }
}

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10_000_000,
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 7,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}"
    )
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

//  Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MongoDB client and database
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context
            .ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);

        var message = string.Join(" ", errors);

        return new BadRequestObjectResult(new { msg = message });
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// To get UserId from jwt token and use it in Apilogs
var jwtKey =
    Environment.GetEnvironmentVariable("Jwt__Key") ?? builder.Configuration["JwtSettings:Key"];

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.Use(
    async (context, next) =>
    {
        await next();

        if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
        {
            context.Response.ContentType = $"{AppStrings.Constants.AppLicationJson}";
            await context.Response.WriteAsync(
                "{\"status\":401,\"message\":\"Unauthorized: Token missing or invalid\"}"
            );
        }
    }
);

//Ordered Middleware , chnaging this order will cause issue in api responses
app.UseMiddleware<RequestIdMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiLoggingMiddleware>();



//api name casing validation middleware
app.Use(async (context, next) =>
{
    var requestPath = context.Request.Path.Value?.TrimEnd('/') ?? string.Empty;
    var endpoint = context.GetEndpoint();

    if (endpoint is RouteEndpoint routeEndpoint)
    {
        var template = routeEndpoint.RoutePattern.RawText?.TrimEnd('/') ?? string.Empty;

        // Extract static part of the template (before any parameters like {id})
        var staticTemplate = "/" + string.Join(
            '/',
            template.Split('/')
                    .TakeWhile(segment => !segment.StartsWith("{"))
        );

        var staticRequestPath = string.Join(
    '/',
    requestPath.Split('/')
               .Take(staticTemplate.Split('/').Length)
).Trim('/');

staticRequestPath = "/" + staticRequestPath;

        if (!string.Equals(staticRequestPath, staticTemplate, StringComparison.Ordinal))
        {
            Console.WriteLine($"❌ Casing mismatch: '{staticRequestPath}' vs '{staticTemplate}'");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
    }

    await next();
});

app.MapControllers();
app.UseCors("AllowAll");

//  Configure Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.Run();

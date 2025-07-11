using System.Text;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Exceptions;
using tripath.Utils;

namespace tripath.MiddleWare
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMongoCollection<ApiLog> _logCollection;

        public ApiLoggingMiddleware(RequestDelegate next, IMongoDatabase database)
        {
            _next = next;
            _logCollection = database.GetCollection<ApiLog>("ApiLogs");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var requestId = context.Items[$"{AppStrings.Constants.RequestId}"]?.ToString();
            var originalBodyStream = context.Response.Body;

            //Get request doesn't get ApiLogs
            if (HttpMethods.IsGet(context.Request.Method))
            {
                await _next(context);
                return;
            }

            try
            {
                // Idempotency check
                if (
                    (
                        request.Method == $"{AppStrings.Methods.POST}"
                        || request.Method == $"{AppStrings.Methods.PUT}"
                        || request.Method == $"{AppStrings.Methods.DELETE}"
                    ) && !string.IsNullOrEmpty(requestId)
                )
                {
                    var exists = await _logCollection
                        .Find(x => x.RequestId == requestId)
                        .FirstOrDefaultAsync();
                    if (exists != null)
                    {
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        context.Response.ContentType = $"{AppStrings.Constants.AppLicationJson}";
                        await context.Response.WriteAsync(
                            System.Text.Json.JsonSerializer.Serialize(
                                new
                                {
                                    statusCode = StatusCodes.Status409Conflict,
                                    message = $"{AppStrings.Messages.RequestProcessed}",
                                    requestId = requestId,
                                }
                            )
                        );
                        return;
                    }
                }

                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                string bodyContent = "";
                if (request.Method == $"{AppStrings.Methods.POST}" || request.Method == $"{AppStrings.Methods.PUT}")
                {
                    request.EnableBuffering();
                    using var reader = new StreamReader(
                        request.Body,
                        Encoding.UTF8,
                        leaveOpen: true
                    );
                    bodyContent = await reader.ReadToEndAsync();
                    request.Body.Position = 0;
                }

                // Proceed to controller
                await _next(context);

                // Read the response body
                responseBody.Seek(0, SeekOrigin.Begin);
                string responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);

                // Extract user claims
                var userId = context.User?.FindFirst($"{AppStrings.Constants.sub}")?.Value
              ?? context.User?.FindFirst($"{AppStrings.Constants.UserId}")?.Value
              ?? ObjectId.Empty.ToString();

                // Log after controller
                var log = new ApiLog
                {
                    RequestId = requestId,
                    UserId = userId,
                    RequestMethod = request.Method,
                    RequestPath = request.Path,
                    RequestQueryParams = request.QueryString.ToString(),
                    RequestBody = bodyContent,
                    ResponseSentDate = DateTime.UtcNow,
                    RequestEntryDate = DateTime.UtcNow,
                    ResponseMessage = responseBodyText,
                    ResponseStatus = context.Response.StatusCode.ToString(),
                };

                await _logCollection.InsertOneAsync(log);

                // Copy response back
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (ApiValidationException ex)
            {
                context.Response.Body = originalBodyStream; //Restore original stream
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = $"{AppStrings.Constants.AppLicationJson}";

                var response = new { status = false, message = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (NotFoundException ex)
            {
                context.Response.Body = originalBodyStream;
                context.Response.StatusCode = 404;
                context.Response.ContentType = $"{AppStrings.Constants.AppLicationJson}";
                var response = new { status = false, message = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.Body = originalBodyStream; // Restore original stream
                context.Response.ContentType = $"{AppStrings.Constants.AppLicationJson}";

                int statusCode = ex switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    ApiValidationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };

                context.Response.StatusCode = statusCode;

                var response = new
                {
                    status = false,
                    message = ex.Message,
                    detail = ex.InnerException?.Message,
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}

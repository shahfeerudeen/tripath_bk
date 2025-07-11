using tripath.Utils;

public class RequestIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

  public async Task InvokeAsync(HttpContext context)
{
    var method = context.Request.Method;

    // Only enforce X-Request-ID for POST, PUT, DELETE
    if (method == HttpMethods.Post || method == HttpMethods.Put || method == HttpMethods.Delete)
    {
        if (!context.Request.Headers.TryGetValue("X-Request-ID", out var requestId) || string.IsNullOrWhiteSpace(requestId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = $"{AppStrings.Constants.AppLicationJson}";

            var errorResponse = new
            {
                status = 400,
                message = $"{AppStrings.Messages.RequestIdMissing}"
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
            return;
        }

        context.Items[$"{AppStrings.Constants.RequestId}"] = requestId.ToString();
    }

    await _next(context);
}

}

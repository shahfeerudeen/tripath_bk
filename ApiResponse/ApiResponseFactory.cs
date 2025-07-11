using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tripath.ApiResponse
{
    public static class ApiResponseFactory
    {
        public static IActionResult CreateSuccessResponse<T>(
            HttpContext context,
            T data,
            string message = "Success",
            int statusCode = 200
        )
        {
            string? requestId = null;

            if (context.Request.Method == HttpMethods.Post)
            {
                requestId = context.Request.Headers["X-Request-ID"].FirstOrDefault();
            }

            return new ObjectResult(
                new ApiResponse<T>
                {
                    Message = message,
                    StatusCode = statusCode,
                    Data = data,
                    RequestId = requestId,
                }
            )
            {
                StatusCode = statusCode,
            };
        }

        public static IActionResult CreateErrorResponse(
            HttpContext context,
            string message,
            int statusCode = 400
        )
        {
            string? requestId = null;

            if (context.Request.Method == HttpMethods.Post)
            {
                requestId = context.Request.Headers["X-Request-ID"].FirstOrDefault();
            }

            return new ObjectResult(
                new ApiResponse<object>
                {
                    Message = message,
                    StatusCode = statusCode,

                    RequestId = requestId,
                }
            )
            {
                StatusCode = statusCode,
            };
        }
    }
}

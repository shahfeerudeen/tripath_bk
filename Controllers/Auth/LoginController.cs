using MediatR;
using Microsoft.AspNetCore.Mvc;
using tripath.ApiResponse;
using tripath.Commands; // Make sure this points to your LoginCommand
using tripath.Models;
using tripath.Queries;
using tripath.Utils;

namespace tripath.Controllers
{
    [ApiController]
    [Route(ApiPaths.Auth.login)]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command) // 
        {
            try
            {
                var result = await _mediator.Send(command); // uses command now
                return ApiResponseFactory.CreateSuccessResponse(
                    HttpContext,
                    result,
                    AppStrings.Messages.LoginSuccess,
                    200
                );
            }
            catch (ArgumentException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 400);
            }
            catch (UnauthorizedAccessException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 401);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"{AppStrings.Constants.Login} {AppStrings.Constants.Exception}: {ex}"
                );
                return ApiResponseFactory.CreateErrorResponse(
                    HttpContext,
                    AppStrings.Messages.somethingWentWrong,
                    500
                );
            }
        }
    }
}

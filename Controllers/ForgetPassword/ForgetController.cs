using MediatR;
using Microsoft.AspNetCore.Mvc;
using tripath.ApiResponse;
using tripath.Commands;
using tripath.Commands.Auth;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Controllers
{
    [ApiController]
    public class ForgetController : ControllerBase
    {
        private readonly IMediator _mediator;


        private readonly IApplicationLogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public ForgetController(IMediator mediator, IApplicationLogRepository logRepository, IUserRepository userRepository)
        {
            _mediator = mediator;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        [Route(ApiPaths.Auth.forgetPassword)]
        [HttpPost]

        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return ApiResponseFactory.CreateSuccessResponse(
                    HttpContext,
                    result,
                    AppStrings.Messages.OtpSent,
                    200
                );
            }
            catch (ArgumentException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 400);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{AppStrings.Constants.ForgetPassword} {AppStrings.Constants.Exception} {ex}");
                return ApiResponseFactory.CreateErrorResponse(
                    HttpContext,
                    AppStrings.Messages.OtpSomethingWrong,
                    500
                );
            }
        }

        [Route(ApiPaths.Auth.resetPassword)]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetUserPasswordCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return ApiResponseFactory.CreateErrorResponse(
                        HttpContext,
                        AppStrings.Messages.InvalidUserIdCantResetPwd,
                        400
                    );
                }
                var user = await _userRepository.GetUserByIdAsync(command.UserId!);
                var userName = user?.UserName ?? command.UserId;
                await _logRepository.LogAsync($"{userName}'s {AppStrings.Messages.PasswordChanged}", command.UserId!);
                return ApiResponseFactory.CreateSuccessResponse(
                    HttpContext,
                    new { success = true },
                    AppStrings.Messages.PasswordReset,
                    200
                );
            }
            catch (ArgumentException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 400);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{AppStrings.Constants.ResetPassword} {AppStrings.Constants.Exception} {ex}");
                return ApiResponseFactory.CreateErrorResponse(
                    HttpContext,
                    AppStrings.Messages.ResetPasswordSomethingWrong,
                    500
                );
            }
        }

        [Route(ApiPaths.Auth.resendOtp)]
        [HttpPost]

        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return ApiResponseFactory.CreateSuccessResponse(
                    HttpContext,
                    result,
                    $"{AppStrings.Messages.OtpSentTo}{command.OtpType}",
                    200
                );
            }
            catch (ArgumentException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 400);
            }
            catch (InvalidOperationException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 429);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{AppStrings.Constants.ResendOtp} {AppStrings.Constants.Exception} {ex}");
                return ApiResponseFactory.CreateErrorResponse(
                    HttpContext,
                    AppStrings.Messages.UnexpectedError,
                    500
                );
            }
        }

        [Route(ApiPaths.Auth.verifyOtp)]

        [HttpPost]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
        {
            try
            {
               var sessionId = await _mediator.Send(command);
            return ApiResponseFactory.CreateSuccessResponse(
                HttpContext,
                new { success = true, sessionId = sessionId },
                AppStrings.Messages.otpVerified,
                200
            );

            }
            catch (ArgumentException ex)
            {
                return ApiResponseFactory.CreateErrorResponse(HttpContext, ex.Message, 400);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Verify Otp Exception: " + ex.ToString());

                return ApiResponseFactory.CreateErrorResponse(
                    HttpContext,
                    AppStrings.Messages.somethingWentWrong,
                    500
                );
            }
        }
    }
}

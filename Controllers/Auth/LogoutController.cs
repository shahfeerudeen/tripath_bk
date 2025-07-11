using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tripath.Commands;
using tripath.Commands.Auth;
using tripath.Utils;

namespace tripath.Controllers
{
    [Authorize]
    [ApiController]
    [Route(ApiPaths.Auth.logout)]
    public class LogoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogoutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.Send(new LogoutCommand());
            return Ok(new
            {
                status = true,
                message = AppStrings.Messages.LogOutSuccess,
                data = result,
            });
        }
    }
}

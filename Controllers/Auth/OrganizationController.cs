using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using tripath.ApiResponse;
using tripath.Models;
using tripath.Queries;
using tripath.Utils;

namespace tripath.Controllers
{
    [ApiController]
    [Route(ApiPaths.Auth.organization)]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _mediator.Send(new GetAllOrganizationsQuery());

            if (organizations == null || organizations.Count == 0)
            {
                // Return empty object instead of null or list
                return ApiResponseFactory.CreateSuccessResponse(
                    HttpContext,
                    new { },
                    AppStrings.Messages.OrganizationNotFound,
                    200
                );
            }

            // Wrap the list in a named object
            return ApiResponseFactory.CreateSuccessResponse(
                HttpContext,
                new { organizations }, // <== this will result in data: { organizations: [...] }
                AppStrings.Messages.OrganizationList,
                200
            );
        }
    }
}

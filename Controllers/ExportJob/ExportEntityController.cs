using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tripath_Logistics_BE.Commands.ExportJob;
using Tripath_Logistics_BE.Queries.ExportJob;

namespace Tripath_Logistics_BE.Controllers.ExportJob
{
    [ApiController]
    [Route("api/[controller]")]
   public class ExportEntityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExportEntityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("upsert")]
    public async Task<IActionResult> Upsert([FromBody] UpsertExportEntityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { status = true, message = "Success", data = result });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllExportEntitiesQuery());
        return Ok(new { status = true, message = "Success", data = result });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetExportEntityByIdQuery(id));
        if (result == null)
            return NotFound(new { status = false, message = "Not Found" });

        return Ok(new { status = true, message = "Success", data = result });
    }
}
}
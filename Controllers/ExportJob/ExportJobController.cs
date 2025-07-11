using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using tripath.Queries.ExportJob;
using tripath.Utils;
using ExportJobModel = tripath.Models.ExportJob.ExportJob;

namespace tripath.Controllers.ExportJob
{
    [ApiController]
    [Route("v1/exporter")]
    public class ExportJobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExportJobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredExportJobs(
            [FromQuery] string? jobno,
            [FromQuery] DateTime? exporterDate,
            [FromQuery] string? exporter
        )
        {
            var query = new GetFilteredExportJobsQuery
            {
                Jobno = jobno,
                ExporterDate = exporterDate,
                Exporter = exporter,
            };

            var result = await _mediator.Send(query);
            return Ok(new ApiResponse<List<ExportJobModel>>(
                200,
                "Filtered export jobs fetched successfully",
                result
            ));
        }

        [HttpPost("createExport")]
        public async Task<IActionResult> Create([FromBody] CombainedExporterRequest request)
        {
            var result = await _mediator.Send(new CreateCombainedExporterCommand { Request = request });

            return Ok(new
            {
                Status = 200,
                Message = "Export job created successfully",
                ExporterId = result
            });
        }
        
[HttpGet("{id}")]
public async Task<IActionResult> GetById(string id)
{
    Console.WriteLine($"Controller: GetById called with ID = {id}");

    try
    {
        var result = await _mediator.Send(new GetCombinedExporterQuery(id));
        return Ok(new { status = true, message = "Success", data = result });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        return NotFound(new { status = false, message = ex.Message });
    }
}

    }
}

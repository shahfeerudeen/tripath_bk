using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tripath.Commands;
using tripath.Models;
using tripath.Queries;
using tripath.Queries.Customer;

namespace tripath.Controllers;

[Route("v1/[controller]")]
public class CustomerMasterController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerMasterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Createcustomer")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateCustomerWithAddressRequest request)
    {
        if (request == null || !ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst("UserId")?.Value;
        var emailExists = await _mediator.Send(
            new CheckCustomerEmailExistsQuery(request.Address.CustomerAddressEmailAddress)
        );

        if (emailExists)
        {
            return Conflict(
                new { status = 409, message = "Email already exists. Customer not created." }
            );
        }
        // Validate ObjectId format (optional but recommended)
        if (!string.IsNullOrEmpty(userId) && userId.Length == 24)
        {
            request.Address.CustomerAddressUpdatedBy = userId;
            // Set other audit fields if needed
        }

        // Set metadata for CustomerMaster
        request.Master.CustomerEntryDate = DateTime.UtcNow;
        request.Master.CustomerUpdateDate = DateTime.UtcNow;

        // Set metadata for CustomerAddress
        request.Address.CustomerAddressUpdatedBy = userId;
        request.Address.CustomerAddressEntryDate = DateTime.UtcNow;
        request.Address.CustomerAddressUpdateDate = DateTime.UtcNow;

        // MediatR command
        var command = new CreateCustomerWithAddressCommand(request.Master, request.Address);
        var createdMaster = await _mediator.Send(command);

        if (createdMaster == null)
            return StatusCode(500, new { status = 500, message = "Customer creation failed." });

        return Ok(
            new
            {
                status = 200,
                message = "Customer created successfully",
                customerId = createdMaster.CustomerId,
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] CustomerMasterUpdateModel customer
    )
    {
        var userId = User.FindFirst("UserId")?.Value;

        customer.CustomerUpdatedBy = userId;
        customer.CustomerUpdateDate = DateTime.UtcNow;

        var command = new UpdateCustomerMasterCommand(id, customer, userId!);
        var result = await _mediator.Send(command);

        if (result == null)
            return NotFound(new { message = "Customer not found or not updated" });

        return Ok(new { status = 200, message = "Customer updated successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var query = new GetCustomerByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { msg = "Customer not found" });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllCustomersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterCustomerWithAddressQuery query)
    {
        var result = await _mediator.Send(query);

        return result != null && result.Count > 0
            ? Ok(
                new
                {
                    statusCode = 200,
                    message = "Success",
                    data = result,
                }
            )
            : NotFound(new { statusCode = 404, message = "No customer records found" });
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomerMaster(string customerId)
    {
        var command = new DeleteCustomerMasterCommand(customerId);
        var result = await _mediator.Send(command);

        if (result)
            return Ok(new { status = 200, message = "Customer deleted successfully" });

        return NotFound(new { status = 404, message = "Customer not found or already deleted" });
    }
}

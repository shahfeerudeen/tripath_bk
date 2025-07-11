using MediatR;
using Microsoft.AspNetCore.Mvc;
using tripath.Commands;
using tripath.Models;
using tripath.Queries;

namespace tripath.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class NewCustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewCustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{type}/{customerId}/{recordId?}")]
        public async Task<IActionResult> Upsert(
            string type,
            string customerId,
            string? recordId,
            [FromBody] object data
        )
        {
            var userId = User.FindFirst("UserId")?.Value;
            switch (type)
            {
                case "DetailAddress":
                    var address =
                        System.Text.Json.JsonSerializer.Deserialize<CustomerDetailAddress>(
                            data.ToString()!
                        );
                    address!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        address.CustomerDetailAddressId = recordId;
                    var addrRes = await _mediator.Send(
                        new UpsertCustomerDetailAddressCommand(address, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = addrRes.CustomerDetailAddressId,
                        }
                    );

                case "Contact":
                    var contact = System.Text.Json.JsonSerializer.Deserialize<CustomerContact>(
                        data.ToString()!
                    );
                    contact!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        contact.CustomerContactId = recordId;
                    var conRes = await _mediator.Send(
                        new UpsertCustomerContactCommand(contact, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = conRes.CustomerContactId,
                        }
                    );

                case "Consignee":
                    var consignee = System.Text.Json.JsonSerializer.Deserialize<CustomerConsignee>(
                        data.ToString()!
                    );
                    consignee!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        consignee.CustomerConsigneeId = recordId;
                    var consRes = await _mediator.Send(
                        new UpsertCustomerConsigneeCommand(consignee, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = consRes.CustomerConsigneeId,
                        }
                    );

                case "Shipper":
                    var shipper = System.Text.Json.JsonSerializer.Deserialize<CustomerShipper>(
                        data.ToString()!
                    );
                    shipper!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        shipper.CustomerShipperId = recordId;
                    var shipRes = await _mediator.Send(
                        new UpsertCustomerShipperCommand(shipper, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = shipRes.CustomerShipperId,
                        }
                    );

                case "Service":
                    var service = System.Text.Json.JsonSerializer.Deserialize<CustomerServices>(
                        data.ToString()!
                    );
                    service!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        service.CustomerServiceId = recordId;
                    var servRes = await _mediator.Send(
                        new UpsertCustomerServiceCommand(service, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = servRes.CustomerServiceId,
                        }
                    );
                case "Carrier":
                    var carrier = System.Text.Json.JsonSerializer.Deserialize<CustomerCarrier>(
                        data.ToString()!
                    );
                    carrier!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        carrier.CustomerCarrierId = recordId;
                    var carRes = await _mediator.Send(
                        new UpsertCustomerCarrierCommand(carrier, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = carRes.CustomerCarrierId,
                        }
                    );
                case "Integration":
                    var integration =
                        System.Text.Json.JsonSerializer.Deserialize<CustomerIntegration>(
                            data.ToString()!
                        );
                    integration!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        integration.CustomerIntegrationId = recordId;
                    var integRes = await _mediator.Send(
                        new UpsertCustomerIntegrationCommand(integration, userId!)
                    );
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = integRes.CustomerIntegrationId,
                        }
                    );
                case "Reg":
                    var reg = System.Text.Json.JsonSerializer.Deserialize<CustomerReg>(
                        data.ToString()!
                    );
                    reg!.CustomerId = customerId;
                    if (!string.IsNullOrEmpty(recordId))
                        reg.CustomerRegistrationId = recordId;
                    var regRes = await _mediator.Send(new UpsertCustomerRegCommand(reg, userId!));
                    return Ok(
                        new
                        {
                            status = true,
                            message = string.IsNullOrEmpty(recordId) ? "Created" : "Updated",
                            id = regRes.CustomerRegistrationId,
                        }
                    );
                default:
                    return BadRequest(new { status = false, message = "Invalid type" });
            }
        }

        [HttpGet("{type}/all")]
        public async Task<IActionResult> GetAll(string type)
        {
            object? result = type switch
            {
                "DetailAddress" => await _mediator.Send(new GetAllCustomerDetailAddressesQuery()),
                "Contact" => await _mediator.Send(new GetAllCustomerContactsQuery()),
                "Consignee" => await _mediator.Send(new GetAllCustomerConsigneesQuery()),
                "Shipper" => await _mediator.Send(new GetAllCustomerShipperQuery()),
                "Service" => await _mediator.Send(new GetAllCustomerServicesQuery()),
                "Carrier" => await _mediator.Send(new GetAllCustomerCarrierQuery()),
                "Integration" => await _mediator.Send(new GetAllCustomerIntegrationQuery()),
                "Reg" => await _mediator.Send(new GetAllCustomerRegQuery()),
                _ => null,
            };

            if (result == null)
            {
                return BadRequest(new { status = false, message = "Invalid type" });
            }

            if (result is IEnumerable<object> list && !list.Any())
            {
                return Ok(new { status = false, message = "No data found" });
            }

            return Ok(result);
        }

        [HttpGet("{type}/bycustomer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(string type, string customerId)
        {
            object? result = type switch
            {
                "DetailAddress" => await _mediator.Send(
                    new GetCustomerDetailAddressesQuery(customerId)
                ),
                "Contact" => await _mediator.Send(
                    new GetCustomerContactsByCustomerIdQuery(customerId)
                ),
                "Consignee" => await _mediator.Send(
                    new GetCustomerConsigneesByCustomerIdQuery(customerId)
                ),
                "Shipper" => await _mediator.Send(
                    new GetCustomerShipperByCustomerIdQuery(customerId)
                ),
                "Service" => await _mediator.Send(
                    new GetCustomerServiceByCustomerIdQuery(customerId)
                ),
                "Carrier" => await _mediator.Send(
                    new GetCustomerCarrierByCustomerIdQuery(customerId)
                ),
                "Integration" => await _mediator.Send(
                    new GetCustomerIntegrationByCustomerIdQuery(customerId)
                ),
                "Reg" => await _mediator.Send(new GetCustomerRegByCustomerIdQuery(customerId)),
                _ => null,
            };

            if (result == null)
            {
                return BadRequest(new { status = false, message = "Invalid type" });
            }

            // Check if the result is a list and is empty
            if (result is IEnumerable<object> list && !list.Any())
            {
                return Ok(new { status = false, message = "No data found" });
            }

            return Ok(result);
        }

        [HttpGet("{type}/byid/{id}")]
        public async Task<IActionResult> GetById(string type, string id)
        {
            object? result = type switch
            {
                "DetailAddress" => await _mediator.Send(new GetCustomerDetailAddressByIdQuery(id)),
                "Contact" => await _mediator.Send(new GetCustomerContactByContactIdQuery(id)),
                "Consignee" => await _mediator.Send(new GetCustomerConsigneeByIdQuery(id)),
                "Shipper" => await _mediator.Send(new GetCustomerShipperByIdQuery(id)),
                "Service" => await _mediator.Send(new GetCustomerServiceByIdQuery(id)),
                "Carrier" => await _mediator.Send(new GetCustomerCarrierByIdQuery(id)),
                "Integration" => await _mediator.Send(new GetCustomerIntegrationByIdQuery(id)),
                "Reg" => await _mediator.Send(new GetCustomerRegByIdQuery(id)),
                _ => null,
            };

            if (result == null)
                switch (type)
                {
                    case "DetailAddress":
                        result = await _mediator.Send(new GetCustomerDetailAddressByIdQuery(id));
                        break;
                    case "Contact":
                        result = await _mediator.Send(new GetCustomerContactByContactIdQuery(id));
                        break;
                    case "Consignee":
                        result = await _mediator.Send(new GetCustomerConsigneeByIdQuery(id));
                        break;
                    case "Shipper":
                        result = await _mediator.Send(new GetCustomerShipperByIdQuery(id));
                        break;
                    case "Service":
                        result = await _mediator.Send(new GetCustomerServiceByIdQuery(id));
                        break;
                    case "Carrier":
                        result = await _mediator.Send(new GetCustomerCarrierByIdQuery(id));
                        break;
                    case "Integration":
                        result = await _mediator.Send(new GetCustomerIntegrationByIdQuery(id));
                        break;
                    case "Reg":
                        result = await _mediator.Send(new GetCustomerRegByIdQuery(id));
                        break;
                    default:
                        return BadRequest(new { status = false, message = "Invalid type" });
                }

            if (result == null)
            {
                return Ok(new { status = false, message = "No data found" });
            }

            return Ok(result);
        }

        [HttpDelete("{type}/{id}")]
        public async Task<IActionResult> Delete(string type, string id)
        {
            bool result;
            string message;

            switch (type)
            {
                case "DetailAddress":
                    result = await _mediator.Send(new DeleteCustomerDetailAddressCommand(id));
                    message = result
                        ? "Customer Detail Address deleted successfully"
                        : "Customer Detail Address not found or already deleted";
                    break;

                case "Contact":
                    result = await _mediator.Send(new DeleteCustomerContactCommand(id));
                    message = result
                        ? "Customer Contact deleted successfully"
                        : "Customer Contact not found or already deleted";
                    break;

                case "Consignee":
                    result = await _mediator.Send(new DeleteCustomerConsigneeCommand(id));
                    message = result
                        ? "Customer Consignee deleted successfully"
                        : "Customer Consignee not found or already deleted";
                    break;

                case "Shipper":
                    result = await _mediator.Send(new DeleteCustomerShipperCommand(id));
                    message = result
                        ? "Customer Shipper deleted successfully"
                        : "Customer Shipper not found or already deleted";
                    break;

                case "Service":
                    result = await _mediator.Send(new DeleteCustomerServiceCommand(id));
                    message = result
                        ? "Customer Service deleted successfully"
                        : "Customer Service not found or already deleted";
                    break;
                case "Carrier":
                    result = await _mediator.Send(new DeleteCustomerCarrierCommand(id));
                    message = result
                        ? "Customer Carrier deleted successfully"
                        : "Customer Carrier not found or already deleted";
                    break;
                case "Integration":
                    result = await _mediator.Send(new DeleteCustomerIntegrationCommand(id));
                    message = result
                        ? "Customer Integration deleted successfully"
                        : "Customer Integration not found or already deleted";
                    break;
                case "Reg":
                    result = await _mediator.Send(new DeleteCustomerRegCommand(id));
                    message = result
                        ? "Customer Reg deleted successfully"
                        : "Customer Reg not found or already deleted";
                    break;

                default:
                    return BadRequest(new { status = false, message = "Invalid type" });
            }

            if (result)
                return Ok(new { status = true, message });
            else
                return NotFound(new { status = false, message });
        }
    }
}

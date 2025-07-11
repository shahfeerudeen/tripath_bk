using MediatR;
using tripath.Models;

public class UpsertCustomerIntegrationCommand : IRequest<CustomerIntegration>
{
    public CustomerIntegration Integration { get; }
    public string CustomerUpdatedBy { get; set; }

    public UpsertCustomerIntegrationCommand(CustomerIntegration integration, string updatedBy)
    {
        Integration = integration;
        CustomerUpdatedBy = updatedBy;
    }
}

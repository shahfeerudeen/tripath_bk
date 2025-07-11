using MediatR;
using tripath.Models;

public class GetCustomerIntegrationByCustomerIdQuery : IRequest<List<CustomerIntegration>>
{
    public string CustomerId { get; }

    public GetCustomerIntegrationByCustomerIdQuery(string customerId)
    {
        CustomerId = customerId;
    }
}

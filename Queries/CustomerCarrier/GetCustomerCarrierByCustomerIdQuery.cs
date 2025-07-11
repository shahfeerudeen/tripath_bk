using MediatR;
using tripath.Models;

namespace tripath.Queries
{
public class GetCustomerCarrierByCustomerIdQuery : IRequest<List<CustomerCarrier>>
{
    public string CustomerId { get; }
    public GetCustomerCarrierByCustomerIdQuery(string customerId)
    {
        CustomerId = customerId;
    }
}
}
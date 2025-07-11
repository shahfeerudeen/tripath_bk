using MediatR;
using tripath.Models;

namespace tripath.Queries
{
   public class GetCustomerContactsByCustomerIdQuery : IRequest<List<CustomerContact>>
{
    public string CustomerId { get; }

    public GetCustomerContactsByCustomerIdQuery(string customerId)
    {
        CustomerId = customerId;
    }
}
}
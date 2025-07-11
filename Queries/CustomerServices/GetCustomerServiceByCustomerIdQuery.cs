using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetCustomerServiceByCustomerIdQuery : IRequest<CustomerServices>
    {
        public string CustomerId { get; }

        public GetCustomerServiceByCustomerIdQuery(string customerId)
        {
            CustomerId = customerId;
        }
    }
}

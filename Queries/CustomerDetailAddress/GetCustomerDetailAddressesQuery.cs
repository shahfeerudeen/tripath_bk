using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetCustomerDetailAddressesQuery : IRequest<List<CustomerDetailAddress>>
    {
        public string CustomerId { get; }

        public GetCustomerDetailAddressesQuery(string customerId)
        {
            CustomerId = customerId;
        }
    }
}

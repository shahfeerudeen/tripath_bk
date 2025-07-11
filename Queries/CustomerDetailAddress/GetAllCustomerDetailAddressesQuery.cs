using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetAllCustomerDetailAddressesQuery : IRequest<List<CustomerDetailAddress>>
    {
    }
}

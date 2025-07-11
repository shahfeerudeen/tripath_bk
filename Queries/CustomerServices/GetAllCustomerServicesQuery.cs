using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetAllCustomerServicesQuery : IRequest<IEnumerable<CustomerServices>> { }
}

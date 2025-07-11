using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetAllCustomerIntegrationQuery : IRequest<List<CustomerIntegration>>
    {
    }
}

using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetCustomerServiceByIdQuery : IRequest<CustomerServices>
    {
        public string CustomerServiceId { get; }

        public GetCustomerServiceByIdQuery(string customerServiceId)
        {
            CustomerServiceId = customerServiceId;
        }
    }
}

using MediatR;
using tripath.Models;

namespace tripath.Queries
{
   public class GetCustomerDetailAddressByIdQuery : IRequest<CustomerDetailAddress>
{
    public string CustomerDetailAddressId { get; }

    public GetCustomerDetailAddressByIdQuery(string customerDetailAddressId)
    {
        CustomerDetailAddressId = customerDetailAddressId;
    }
}
}


using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetCustomerCarrierByIdQuery : IRequest<CustomerCarrier>
{
    public string CustomerCarrierId { get; }
    public GetCustomerCarrierByIdQuery(string customerCarrierId)
    {
        CustomerCarrierId = customerCarrierId;
    }
}

}
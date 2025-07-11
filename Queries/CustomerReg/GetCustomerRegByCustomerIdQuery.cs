using MediatR;
using tripath.Models;

namespace tripath.Queries
{
   public class GetCustomerRegByCustomerIdQuery : IRequest<List<CustomerReg>>
{
    public string CustomerId { get; }
    public GetCustomerRegByCustomerIdQuery(string customerId) => CustomerId = customerId;
}

}
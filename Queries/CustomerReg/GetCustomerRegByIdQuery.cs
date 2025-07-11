using MediatR;
using tripath.Models;

namespace tripath.Queries
{
   public class GetCustomerRegByIdQuery : IRequest<CustomerReg>
{
    public string Id { get; }
    public GetCustomerRegByIdQuery(string id) => Id = id;
}
}
using MediatR;
using tripath.Models;

namespace tripath.Queries
{
   public class GetAllCustomerRegQuery : IRequest<List<CustomerReg>> { }

}
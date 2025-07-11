using MediatR;
using tripath.Models;

namespace tripath.Queries
{
  public class GetAllCustomerContactsQuery : IRequest<List<CustomerContact>> { }

}
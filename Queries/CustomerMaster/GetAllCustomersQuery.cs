using System.Collections.Generic;
using MediatR;
using tripath.Models;

namespace tripath.Queries.Customer
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerWithAddressResponse>> { }
}

using MediatR;
using tripath.Models;

namespace tripath.Queries.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerWithAddressResponse>
    {
        public string Id { get; }

        public GetCustomerByIdQuery(string id)
        {
            Id = id;
        }
    }
}

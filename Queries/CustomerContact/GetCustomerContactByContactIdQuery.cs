using MediatR;
using tripath.Models;

namespace tripath.Queries
{public class GetCustomerContactByContactIdQuery : IRequest<CustomerContact>
{
    public string ContactId { get; }

    public GetCustomerContactByContactIdQuery(string contactId)
    {
        ContactId = contactId;
    }
}

}
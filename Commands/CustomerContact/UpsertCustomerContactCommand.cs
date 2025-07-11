using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpsertCustomerContactCommand : IRequest<CustomerContact>
    {
        public CustomerContact CustomerContact { get; }
        public string CustomerUpdatedBy { get; set; }

        public UpsertCustomerContactCommand(CustomerContact contact, string updatedBy)
        {
            CustomerContact = contact;
            CustomerUpdatedBy = updatedBy;
        }
    }
}
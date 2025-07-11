using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpsertCustomerServiceCommand : IRequest<CustomerServices>
    {
        public CustomerServices Service { get; }
        public string CustomerUpdatedBy { get; set; }

        public UpsertCustomerServiceCommand(CustomerServices service, string updatedBy)
        {
            Service = service;
            CustomerUpdatedBy = updatedBy;
        }
    }
}

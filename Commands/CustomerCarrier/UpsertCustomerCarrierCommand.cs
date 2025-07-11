using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpsertCustomerCarrierCommand : IRequest<CustomerCarrier>
    {
        public CustomerCarrier Customer { get; }
        public string CustomerUpdatedBy { get; set; }

        public UpsertCustomerCarrierCommand(CustomerCarrier customer, string updatedBy)
        {
            Customer = customer;
            CustomerUpdatedBy = updatedBy;
        }

    }
}
using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpsertCustomerDetailAddressCommand : IRequest<CustomerDetailAddress>
    {
        public CustomerDetailAddress Customer { get; }
        public string CustomerUpdatedBy { get; set; }

        public UpsertCustomerDetailAddressCommand(CustomerDetailAddress customer, string updatedBy)
        {
            Customer = customer;
            CustomerUpdatedBy = updatedBy;
        }

    }
}
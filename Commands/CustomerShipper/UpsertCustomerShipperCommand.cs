using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpsertCustomerShipperCommand : IRequest<CustomerShipper>
    {
        public CustomerShipper Customer { get; }
        public string CustomerUpdatedBy { get; set; }

        public UpsertCustomerShipperCommand(CustomerShipper customer, string updatedBy)
        {
            Customer = customer;
            CustomerUpdatedBy = updatedBy;
        }
    }
}
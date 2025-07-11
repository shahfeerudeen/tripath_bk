using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpsertCustomerConsigneeCommand : IRequest<CustomerConsignee>
    {
        public CustomerConsignee Consignee { get; }
        public string CustomerUpdatedBy { get; set; }

        public UpsertCustomerConsigneeCommand(CustomerConsignee consignee, string updatedBy)
        {
            Consignee = consignee;
            CustomerUpdatedBy = updatedBy;
        }
    }
}
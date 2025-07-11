using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpdateCustomerMasterCommand : IRequest<CustomerMaster?>
    {
        public string CustomerId { get; set; }
            public string UpdatedBy { get; set; }
        public CustomerMasterUpdateModel UpdatedCustomer { get; set; }

        public UpdateCustomerMasterCommand(string customerId, CustomerMasterUpdateModel updatedCustomer,string updatedBy)
        {
            CustomerId = customerId;
            UpdatedCustomer = updatedCustomer;
            UpdatedBy= updatedBy;
        }
    }
}

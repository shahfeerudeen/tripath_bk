using MediatR;
using tripath.Models;

public class UpsertCustomerRegCommand : IRequest<CustomerReg>
{
    public CustomerReg CustomerReg { get; }
    public string CustomerUpdatedBy { get; set; }
    public UpsertCustomerRegCommand(CustomerReg reg, string updatedBy)
     { 
        CustomerReg = reg;
          CustomerUpdatedBy = updatedBy;
         }
}

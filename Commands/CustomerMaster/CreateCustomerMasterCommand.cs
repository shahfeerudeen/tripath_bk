using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class CreateCustomerWithAddressCommand : IRequest<CustomerMaster>
{
    public CustomerMaster Master { get; set; }
     public CustomerAddress Address { get; set; }

    public CreateCustomerWithAddressCommand(CustomerMaster master, CustomerAddress address)
    {
        Master = master;
        Address = address;
    }
}

}

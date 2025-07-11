using MediatR;

namespace tripath.Commands
{
 
public class DeleteCustomerRegCommand : IRequest<bool>
{
    public string CustomerRegistrationId { get; }
    public DeleteCustomerRegCommand(string id) => CustomerRegistrationId = id;
}
}
using MediatR;

namespace tripath.Commands
{
    public class DeleteCustomerServiceCommand : IRequest<bool>
    {
        public string CustomerServiceId { get; }

        public DeleteCustomerServiceCommand(string customerServiceId)
        {
            CustomerServiceId = customerServiceId;
        }
    }
}

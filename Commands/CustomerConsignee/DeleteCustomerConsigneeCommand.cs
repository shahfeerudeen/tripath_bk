using MediatR;

namespace tripath.Commands
{
    public class DeleteCustomerConsigneeCommand : IRequest<bool>
    {
        public string CustomerConsigneeId { get; }

        public DeleteCustomerConsigneeCommand(string id)
        {
            CustomerConsigneeId = id;
        }
    }
}

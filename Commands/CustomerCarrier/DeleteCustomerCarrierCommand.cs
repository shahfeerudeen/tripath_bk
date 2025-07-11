using MediatR;

namespace tripath.Commands
{
    public class DeleteCustomerCarrierCommand : IRequest<bool>
    {
        public string CustomerCarrierId { get; }

        public DeleteCustomerCarrierCommand(string id)
        {
            CustomerCarrierId = id;
        }
    }
}

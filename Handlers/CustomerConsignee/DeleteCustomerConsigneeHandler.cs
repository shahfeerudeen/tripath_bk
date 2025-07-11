using MediatR;
using tripath.Commands;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class DeleteCustomerConsigneeHandler : IRequestHandler<DeleteCustomerConsigneeCommand, bool>
    {
        private readonly ICustomerConsigneeRepository _repository;

        public DeleteCustomerConsigneeHandler(ICustomerConsigneeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCustomerConsigneeCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.CustomerConsigneeId);
        }
    }
}

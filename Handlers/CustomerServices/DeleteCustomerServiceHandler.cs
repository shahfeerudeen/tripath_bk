using MediatR;
using tripath.Commands;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class DeleteCustomerServiceHandler : IRequestHandler<DeleteCustomerServiceCommand, bool>
    {
        private readonly ICustomerServiceRepository _repository;

        public DeleteCustomerServiceHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeletedAsync(request.CustomerServiceId);
            return result;
        }
    }
}

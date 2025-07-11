using MediatR;
using tripath.Commands;

namespace tripath.Handlers
{
   public class DeleteCustomerRegHandler : IRequestHandler<DeleteCustomerRegCommand, bool>
{
    private readonly ICustomerRegRepository _repository;
    public DeleteCustomerRegHandler(ICustomerRegRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteCustomerRegCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.CustomerRegistrationId);
    }
}

}
using MediatR;
using tripath.Repositories;

public class DeleteCustomerContactHandler : IRequestHandler<DeleteCustomerContactCommand, bool>
{
    private readonly ICustomerContactRepository _repository;

    public DeleteCustomerContactHandler(ICustomerContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCustomerContactCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.CustomerContactId);
    }
}

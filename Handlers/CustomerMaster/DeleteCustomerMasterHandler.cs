using MediatR;
using tripath.Repositories;

public class DeleteCustomerMasterHandler : IRequestHandler<DeleteCustomerMasterCommand, bool>
{
    private readonly ICustomerMasterRepository _repository;

    public DeleteCustomerMasterHandler(ICustomerMasterRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCustomerMasterCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.CustomerId);
    }
}

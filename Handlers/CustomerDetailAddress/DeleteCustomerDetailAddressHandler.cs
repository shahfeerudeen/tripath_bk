using MediatR;
using tripath.Exceptions; 
using tripath.Repositories;

public class DeleteCustomerDetailAddressHandler : IRequestHandler<DeleteCustomerDetailAddressCommand, bool>
{
    private readonly ICustomerDetailAddressRepository _repository;

    public DeleteCustomerDetailAddressHandler(ICustomerDetailAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCustomerDetailAddressCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.CustomerDetailAddressId))
            throw new NotFoundException("CustomerDetailAddressId is required.");

        var deleted = await _repository.DeleteAsync(request.CustomerDetailAddressId);
        if (!deleted)
            throw new NotFoundException("CustomerDetailAddress not found.");

        return true;
    }
}

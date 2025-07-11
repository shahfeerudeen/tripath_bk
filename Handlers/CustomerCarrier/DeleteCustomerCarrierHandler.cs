using MediatR;
using tripath.Commands;

namespace tripath.Handlers
{
 public class DeleteCustomerCarrierHandler : IRequestHandler<DeleteCustomerCarrierCommand, bool>
{
    private readonly ICustomerCarrierRepository _repository;

    public DeleteCustomerCarrierHandler(ICustomerCarrierRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCustomerCarrierCommand request, CancellationToken cancellationToken)
    {
        return await _repository.SoftDeleteAsync(request.CustomerCarrierId);
    }
}

}
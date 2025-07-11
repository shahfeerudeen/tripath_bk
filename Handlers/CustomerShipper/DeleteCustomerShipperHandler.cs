using MediatR;
using tripath.Repositories;

namespace tripath.Commands
{
  public class DeleteCustomerShipperHandler : IRequestHandler<DeleteCustomerShipperCommand, bool>
{
    private readonly ICustomerShipperRepository _repository;

    public DeleteCustomerShipperHandler(ICustomerShipperRepository repo)
    {
        _repository = repo;
    }

    public async Task<bool> Handle(DeleteCustomerShipperCommand request, CancellationToken cancellationToken)
        => await _repository.DeleteAsync(request.CustomerShipperId);
}
}
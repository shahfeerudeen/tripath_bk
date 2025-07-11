using MediatR;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetCustomerShipperByIdHandler : IRequestHandler<GetCustomerShipperByIdQuery, CustomerShipper>
{
    private readonly ICustomerShipperRepository _repository;
    public GetCustomerShipperByIdHandler(ICustomerShipperRepository repo) => _repository = repo;

   public async Task<CustomerShipper> Handle(GetCustomerShipperByIdQuery request, CancellationToken cancellationToken)
{
    var result = await _repository.GetByIdAsync(request.CustomerShipperId);
    if (result is null)
        throw new KeyNotFoundException($"CustomerShipper with ID {request.CustomerShipperId} not found.");

    return result;
}
}
}
using MediatR;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
   public class GetCustomerShipperByCustomerIdHandler : IRequestHandler<GetCustomerShipperByCustomerIdQuery, List<CustomerShipper>>
{
    private readonly ICustomerShipperRepository _repository;
    public GetCustomerShipperByCustomerIdHandler(ICustomerShipperRepository repo) => _repository = repo;

    public async Task<List<CustomerShipper>> Handle(GetCustomerShipperByCustomerIdQuery request, CancellationToken cancellationToken)
        => await _repository.GetByCustomerIdAsync(request.CustomerId);
}
}
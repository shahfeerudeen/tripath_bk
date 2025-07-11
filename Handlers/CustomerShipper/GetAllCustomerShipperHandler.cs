using MediatR;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetAllCustomerShipperHandler : IRequestHandler<GetAllCustomerShipperQuery, List<CustomerShipper>>
{
    private readonly ICustomerShipperRepository _repository;
    public GetAllCustomerShipperHandler(ICustomerShipperRepository repo) => _repository = repo;

    public async Task<List<CustomerShipper>> Handle(GetAllCustomerShipperQuery request, CancellationToken cancellationToken)
        => await _repository.GetAllAsync();
}

}
using MediatR;
using tripath.Models;

namespace tripath.Handlers
{
   public class GetAllCustomerCarrierHandler : IRequestHandler<GetAllCustomerCarrierQuery, List<CustomerCarrier>>
{
    private readonly ICustomerCarrierRepository _repository;

    public GetAllCustomerCarrierHandler(ICustomerCarrierRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerCarrier>> Handle(GetAllCustomerCarrierQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
}
using MediatR;
using tripath.Models;
using tripath.Queries;

namespace tripath.Handlers
{
   public class GetCustomerCarrierByCustomerIdHandler : IRequestHandler<GetCustomerCarrierByCustomerIdQuery, List<CustomerCarrier>>
{
    private readonly ICustomerCarrierRepository _repository;

    public GetCustomerCarrierByCustomerIdHandler(ICustomerCarrierRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerCarrier>> Handle(GetCustomerCarrierByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCustomerIdAsync(request.CustomerId);
    }
}
}
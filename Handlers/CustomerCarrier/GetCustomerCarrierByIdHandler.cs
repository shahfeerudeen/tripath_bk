using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.Models;
using tripath.Queries;

namespace tripath.Handlers
{
   public class GetCustomerCarrierByIdHandler : IRequestHandler<GetCustomerCarrierByIdQuery, CustomerCarrier?>
{
    private readonly ICustomerCarrierRepository _repository;

    public GetCustomerCarrierByIdHandler(ICustomerCarrierRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerCarrier?> Handle(GetCustomerCarrierByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.CustomerCarrierId);
    }
}
}
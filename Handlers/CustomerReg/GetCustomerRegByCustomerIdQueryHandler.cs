using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.Models;
using tripath.Queries;

namespace tripath.Handlers
{
   public class GetCustomerRegByCustomerIdQueryHandler : IRequestHandler<GetCustomerRegByCustomerIdQuery, List<CustomerReg>>
{
    private readonly ICustomerRegRepository _repository;
    public GetCustomerRegByCustomerIdQueryHandler(ICustomerRegRepository repository) => _repository = repository;

    public async Task<List<CustomerReg>> Handle(GetCustomerRegByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCustomerIdAsync(request.CustomerId);
    }
}
}
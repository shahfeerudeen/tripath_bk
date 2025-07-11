using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.Models;
using tripath.Queries;

namespace tripath.Handlers
{
   public class GetCustomerRegByIdQueryHandler : IRequestHandler<GetCustomerRegByIdQuery, CustomerReg>
{
    private readonly ICustomerRegRepository _repository;
    public GetCustomerRegByIdQueryHandler(ICustomerRegRepository repository) => _repository = repository;

    public async Task<CustomerReg> Handle(GetCustomerRegByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
}
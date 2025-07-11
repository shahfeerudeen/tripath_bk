using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
   public class GetCustomerContactsByCustomerIdHandler : IRequestHandler<GetCustomerContactsByCustomerIdQuery, List<CustomerContact>>
{
    private readonly ICustomerContactRepository _repository;

    public GetCustomerContactsByCustomerIdHandler(ICustomerContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerContact>> Handle(GetCustomerContactsByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCustomerIdAsync(request.CustomerId);
    }
}

}
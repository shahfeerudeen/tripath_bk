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
   public class GetCustomerContactByContactIdHandler : IRequestHandler<GetCustomerContactByContactIdQuery, CustomerContact>
{
    private readonly ICustomerContactRepository _repository;

    public GetCustomerContactByContactIdHandler(ICustomerContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerContact> Handle(GetCustomerContactByContactIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.ContactId);
    }
}

}
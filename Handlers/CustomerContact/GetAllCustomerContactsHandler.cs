using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
   public class GetAllCustomerContactsHandler : IRequestHandler<GetAllCustomerContactsQuery, List<CustomerContact>>
{
    private readonly ICustomerContactRepository _repository;

    public GetAllCustomerContactsHandler(ICustomerContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerContact>> Handle(GetAllCustomerContactsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

}
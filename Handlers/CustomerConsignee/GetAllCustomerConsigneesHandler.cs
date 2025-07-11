using MediatR;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
   public class GetAllCustomerConsigneesHandler : IRequestHandler<GetAllCustomerConsigneesQuery, List<CustomerConsignee>>
{
    private readonly ICustomerConsigneeRepository _repository;

    public GetAllCustomerConsigneesHandler(ICustomerConsigneeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerConsignee>> Handle(GetAllCustomerConsigneesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

}
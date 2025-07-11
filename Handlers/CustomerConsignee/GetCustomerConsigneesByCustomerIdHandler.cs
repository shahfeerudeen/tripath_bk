using MediatR;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetCustomerConsigneesByCustomerIdHandler : IRequestHandler<GetCustomerConsigneesByCustomerIdQuery, List<CustomerConsignee>>
{
    private readonly ICustomerConsigneeRepository _repository;

    public GetCustomerConsigneesByCustomerIdHandler(ICustomerConsigneeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerConsignee>> Handle(GetCustomerConsigneesByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCustomerIdAsync(request.CustomerId);
    }
}

}
using MediatR;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
   public class GetCustomerConsigneeByIdHandler : IRequestHandler<GetCustomerConsigneeByIdQuery, CustomerConsignee>
{
    private readonly ICustomerConsigneeRepository _repository;

    public GetCustomerConsigneeByIdHandler(ICustomerConsigneeRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerConsignee> Handle(GetCustomerConsigneeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.CustomerConsigneeId);
    }
}

}
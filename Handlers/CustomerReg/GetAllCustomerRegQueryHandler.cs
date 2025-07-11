using MediatR;
using tripath.Models;
using tripath.Queries;


public class GetAllCustomerRegQueryHandler : IRequestHandler<GetAllCustomerRegQuery, List<CustomerReg>>
{
    private readonly ICustomerRegRepository _repository;
    public GetAllCustomerRegQueryHandler(ICustomerRegRepository repository) => _repository = repository;

    public async Task<List<CustomerReg>> Handle(GetAllCustomerRegQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
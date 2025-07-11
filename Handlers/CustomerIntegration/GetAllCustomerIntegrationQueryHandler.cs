using MediatR;
using tripath.Models;
using tripath.Queries;

public class GetAllCustomerIntegrationQueryHandler : IRequestHandler<GetAllCustomerIntegrationQuery, List<CustomerIntegration>>
{
    private readonly ICustomerIntegrationRepository _repository;

    public GetAllCustomerIntegrationQueryHandler(ICustomerIntegrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerIntegration>> Handle(GetAllCustomerIntegrationQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

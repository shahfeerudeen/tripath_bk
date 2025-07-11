using MediatR;
using tripath.Models;

public class GetCustomerIntegrationByCustomerIdQueryHandler : IRequestHandler<GetCustomerIntegrationByCustomerIdQuery, List<CustomerIntegration>>
{
    private readonly ICustomerIntegrationRepository _repository;

    public GetCustomerIntegrationByCustomerIdQueryHandler(ICustomerIntegrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CustomerIntegration>> Handle(GetCustomerIntegrationByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCustomerIdAsync(request.CustomerId);
    }
}

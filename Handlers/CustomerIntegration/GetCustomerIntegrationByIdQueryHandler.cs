using MediatR;
using tripath.Models;

public class GetCustomerIntegrationByIdQueryHandler : IRequestHandler<GetCustomerIntegrationByIdQuery, CustomerIntegration?>
{
    private readonly ICustomerIntegrationRepository _repository;

    public GetCustomerIntegrationByIdQueryHandler(ICustomerIntegrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerIntegration?> Handle(GetCustomerIntegrationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

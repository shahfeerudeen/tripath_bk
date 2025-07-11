using MediatR;

namespace tripath.Handlers
{
  public class DeleteCustomerIntegrationHandler : IRequestHandler<DeleteCustomerIntegrationCommand, bool>
{
    private readonly ICustomerIntegrationRepository _repository;

    public DeleteCustomerIntegrationHandler(ICustomerIntegrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCustomerIntegrationCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.CustomerIntegrationId);
    }
}
}
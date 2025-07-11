using MediatR;

public class DeleteCustomerIntegrationCommand : IRequest<bool>
{
    public string CustomerIntegrationId { get; }

    public DeleteCustomerIntegrationCommand(string id)
    {
        CustomerIntegrationId = id;
    }
}

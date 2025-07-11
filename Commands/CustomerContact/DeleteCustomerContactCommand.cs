using MediatR;

public class DeleteCustomerContactCommand : IRequest<bool>
{
    public string CustomerContactId { get; }

    public DeleteCustomerContactCommand(string id)
    {
        CustomerContactId = id;
    }
}

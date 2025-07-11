using MediatR;

public class DeleteCustomerMasterCommand : IRequest<bool>
{
    public string CustomerId { get; }

    public DeleteCustomerMasterCommand(string customerId)
    {
        CustomerId = customerId;
    }
}

using MediatR;

public class DeleteCustomerDetailAddressCommand : IRequest<bool>
{
    public string CustomerDetailAddressId { get; }

    public DeleteCustomerDetailAddressCommand(string id)
    {
        CustomerDetailAddressId = id;
    }
}
using MediatR;

public class DeleteCustomerShipperCommand : IRequest<bool>
{
    public string CustomerShipperId { get; }
    public DeleteCustomerShipperCommand(string id) => CustomerShipperId = id;
}
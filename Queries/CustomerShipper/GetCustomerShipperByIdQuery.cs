using MediatR;
using tripath.Models;

public class GetCustomerShipperByIdQuery : IRequest<CustomerShipper>
{
    public string CustomerShipperId { get; }
    public GetCustomerShipperByIdQuery(string id) => CustomerShipperId = id;
}
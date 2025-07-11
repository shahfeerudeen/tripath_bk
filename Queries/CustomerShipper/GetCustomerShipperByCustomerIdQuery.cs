using MediatR;
using tripath.Models;

public class GetCustomerShipperByCustomerIdQuery : IRequest<List<CustomerShipper>>
{
    public string CustomerId { get; }
    public GetCustomerShipperByCustomerIdQuery(string id) => CustomerId = id;
}
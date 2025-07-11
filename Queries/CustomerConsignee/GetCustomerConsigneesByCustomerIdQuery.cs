using MediatR;
using tripath.Models;

public class GetCustomerConsigneesByCustomerIdQuery : IRequest<List<CustomerConsignee>>
{
    public string CustomerId { get; }
    public GetCustomerConsigneesByCustomerIdQuery(string customerId)
    {
        CustomerId = customerId;
    }
}

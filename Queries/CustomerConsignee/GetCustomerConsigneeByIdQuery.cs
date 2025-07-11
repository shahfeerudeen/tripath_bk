using MediatR;
using tripath.Models;

public class GetCustomerConsigneeByIdQuery : IRequest<CustomerConsignee>
{
    public string CustomerConsigneeId { get; }
    public GetCustomerConsigneeByIdQuery(string customerConsigneeId)
    {
        CustomerConsigneeId = customerConsigneeId;
    }
}

using MediatR;
using tripath.Models;

public class GetCustomerIntegrationByIdQuery : IRequest<CustomerIntegration?>
{
    public string Id { get; }

    public GetCustomerIntegrationByIdQuery(string id)
    {
        Id = id;
    }
}

using MediatR;
using tripath.Models;

public class CheckCustomerEmailExistsQuery : IRequest<bool>
{
    public string Email { get; set; }

    public CheckCustomerEmailExistsQuery(string email)
    {
        Email = email;
    }
}

using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class CreateUserLogCommand : IRequest<UserLog>
    {
        public string? UserId { get; set; }
        public string? IPAddress { get; set; }
        public string? Status { get; set; } 
    }
}
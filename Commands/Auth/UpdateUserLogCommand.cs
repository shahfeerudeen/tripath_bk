using MediatR;
using tripath.Models;

namespace tripath.Commands
{
    public class UpdateUserLogCommand : IRequest<UserLog>
    {
        public string? UserId { get; set; }
      public string?  Status { get; set; } // "O" for logout
    }
}
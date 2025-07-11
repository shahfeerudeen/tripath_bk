using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class LogoutQuery : IRequest<UserLog>
    {
        public LogoutQuery() { }
    }
}

using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetUserByUserIdQuery : IRequest<UserManagement>
    {
        public string UserId { get; set; }

        public GetUserByUserIdQuery(string userId)
        {
            UserId = userId.Trim();
        }
    }
}

using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUserNameQuery, UserManagement>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByUserNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserManagement> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByUserNameAsync(request.UserName);
        }
    }
}

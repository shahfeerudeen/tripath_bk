using MediatR;
using Microsoft.AspNetCore.Http;
using tripath.Commands;
using tripath.Commands.Auth;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers.Auth
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, UserLog>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserDataDetailsRepository _userDataDetailsRepository;

        public LogoutHandler(
            IUserRepository userRepository,
            IUserLogRepository userLogRepository,
            IUserDataDetailsRepository userDataDetailsRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userLogRepository = userLogRepository;
            _userDataDetailsRepository = userDataDetailsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserLog> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // Extract UserId from JWT claims
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(AppStrings.Constants.sub)?.Value
                      ?? _httpContextAccessor?.HttpContext?.User?.FindFirst(AppStrings.Constants.UserId)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException(AppStrings.Messages.UserIdNotFound);

            // Fetch user from repository
            var (user, _) = await _userRepository.GetUserWithMasterByUserIdAsync(userId);
            if (user == null)
                throw new Exception(AppStrings.Messages.UserNotFound);

            // Update user log status to "O" (logout)
            var updatedLog = await _userLogRepository.UpdateStatusAsync(user.UserId!, $"{AppStrings.Status.O}");
            if (updatedLog == null)
                throw new Exception(AppStrings.Messages.UserLogoutFail);

            // Clear token in UserDataDetails
            await _userDataDetailsRepository.UpdateUserTokenAsync(user.UserId!, null);

            return updatedLog;
        }
    }
}

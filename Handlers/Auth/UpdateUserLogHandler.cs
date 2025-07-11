using MediatR;
using tripath.Commands;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpdateUserLogHandler : IRequestHandler<UpdateUserLogCommand, UserLog>
    {
        private readonly IUserLogRepository _userLogRepository;
        private readonly IUserRepository _userRepository;

        public UpdateUserLogHandler(
            IUserLogRepository userLogRepository,
            IUserRepository userRepository
        )
        {
            _userLogRepository = userLogRepository;
            _userRepository = userRepository;
        }

        public async Task<UserLog> Handle(
            UpdateUserLogCommand request,
            CancellationToken cancellationToken
        )
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                throw new ArgumentException($"{AppStrings.Messages.UIdRequired}");

            // Automatically set logout status
            var updatedLog = await _userLogRepository.UpdateStatusAsync(request.UserId, "O");

            // Invalidate token in UserManagement collection
            await _userRepository.UpdateUserTokenAsync(request.UserId, null);

            return updatedLog;
        }
    }
}

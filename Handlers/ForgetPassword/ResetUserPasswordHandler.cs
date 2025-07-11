using MediatR;
using MongoDB.Bson;
using tripath.Commands.Auth;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers.Auth
{
    public class ResetUserPasswordHandler : IRequestHandler<ResetUserPasswordCommand, bool>
    {
        private readonly IUserRepository _repository;
        private readonly IOtpRepository _otpRepository;

        public ResetUserPasswordHandler(IUserRepository repository, IOtpRepository otpRepository)
        {
            _repository = repository;
            _otpRepository = otpRepository;
        }

        public async Task<bool> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //  Basic required field validation
            var missingFields = new List<string>();
            if (string.IsNullOrWhiteSpace(request.UserId)) missingFields.Add($"{AppStrings.Constants.UserId}");
            if (string.IsNullOrWhiteSpace(request.NewPassword)) missingFields.Add($"{AppStrings.Constants.newPassword}");
            if (string.IsNullOrWhiteSpace(request.SessionId)) missingFields.Add($"{AppStrings.Constants.sessionId}");

            if (missingFields.Count > 0)
            {
                string validationMessage = missingFields.Count switch
                {
                    1 => $"{missingFields[0]} {AppStrings.Messages.isRequired}",
                    2 => $"{missingFields[0]} {AppStrings.Constants.and} {missingFields[1]} {AppStrings.Messages.areRequired}",
                    _ => $"{string.Join(", ", missingFields.Take(missingFields.Count - 1))}, {AppStrings.Constants.and} {missingFields.Last()} {AppStrings.Messages.areRequired}"
                };
                throw new ArgumentException(validationMessage);
            }

            //  Validate ObjectId format
            if (!ObjectId.TryParse(request.UserId, out var objectId))
                throw new ArgumentException(AppStrings.Messages.InvalidUserId);

            //  Check if user exists
            var user = await _repository.GetPasswordByIdAsync(objectId);
            if (user == null)
                throw new ArgumentException(AppStrings.Messages.UserNotFound);

            //  Validate OTP session
            var otpSession = await _otpRepository.GetOtpBySessionIdAsync(request.SessionId!);
            if (otpSession == null || !otpSession.IsOtpVerified || otpSession.SessionExpiry < DateTime.UtcNow)
                throw new ArgumentException($"{AppStrings.Messages.InvalidOrExpiredSessionId}");

            //Hash and update password
            user.UserPassword = HashPasswordMD5(request.NewPassword!);
            user.UserUpdateDate = DateTime.UtcNow;

            return await _repository.ResetPasswordUpdateAsync(user);
        }

        private string HashPasswordMD5(string password)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hashBytes = md5.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
        }
    }
}

using MediatR;
using MongoDB.Bson;
using tripath.Commands;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, string>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IUserRepository _userRepository;

        public VerifyOtpHandler(IOtpRepository otpRepository, IUserRepository userRepository)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            // Basic null or whitespace validation
            var missingFields = new List<string>();
            if (string.IsNullOrWhiteSpace(request.UserId)) missingFields.Add($"{AppStrings.Constants.UserId}");
            if (string.IsNullOrWhiteSpace(request.Otp)) missingFields.Add($"{AppStrings.Constants.Otp}");
            if (string.IsNullOrWhiteSpace(request.otpType)) missingFields.Add($"{AppStrings.Constants.OtpType}");

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

            // Validate OtpType
            if (!new[] { $"{AppStrings.Constants.email}", $"{AppStrings.Constants.phone}" }.Contains(request.otpType!.ToLower()))
                throw new ArgumentException(AppStrings.Messages.InvalidOtpType);

            // Validate UserId format
            if (!ObjectId.TryParse(request.UserId, out _))
                throw new ArgumentException(AppStrings.Messages.InvalidUserId);

            // Check if user exists
            var (user, _) = await _userRepository.GetUserWithMasterByUserIdAsync(request.UserId!);
            if (user == null)
                throw new ArgumentException(AppStrings.Messages.UserNotFound);

            // Validate OTP
            var otpEntry = await _otpRepository.GetOtpByUserIdOtpAndTypeAsync(
                request.UserId!, request.Otp!, request.otpType
            );

            if (otpEntry == null)
                throw new ArgumentException(AppStrings.Messages.InvalidOtp);

            if ((DateTime.UtcNow - otpEntry.OtpEntryDate).TotalMinutes > 5)
                throw new ArgumentException(AppStrings.Messages.OtpExpired);

            // Generate session
            string sessionId = Guid.NewGuid().ToString();
            otpEntry.IsOtpVerified = true;
            otpEntry.SessionId = sessionId;
            otpEntry.SessionExpiry = DateTime.UtcNow.AddMinutes(15);

            await _otpRepository.UpdateOtpSessionAsync(
                otpEntry.Id!, sessionId, true, otpEntry.SessionExpiry.Value
            );

            return sessionId;
        }
    }
}

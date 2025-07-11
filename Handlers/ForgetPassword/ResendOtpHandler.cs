using MediatR;
using MongoDB.Bson;
using tripath.Commands;
using tripath.Models;
using tripath.Repositories;
using tripath.Services;
using tripath.Utils;

namespace tripath.Handlers
{
    public class ResendOtpHandler : IRequestHandler<ResendOtpCommand, object>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IOtpService _otpService;
        private readonly IUserRepository _userRepository;

        public ResendOtpHandler(
            IOtpRepository otpRepository,
            IOtpService otpService,
            IUserRepository userRepository
        )
        {
            _otpRepository = otpRepository;
            _otpService = otpService;
            _userRepository = userRepository;
        }

        public async Task<object> Handle(
            ResendOtpCommand request,
            CancellationToken cancellationToken
        )
        {
            //  Validate required fields
            var missingFields = new[]
            {
                new { Field = request.UserId, Name = AppStrings.Constants.UserId },
                new { Field = request.OtpType, Name = AppStrings.Messages.OtpTypeEmailOrPhone },
            }
                .Where(x => string.IsNullOrWhiteSpace(x.Field))
                .Select(x => x.Name)
                .ToList();

            if (missingFields.Any())
                throw new ArgumentException(
                    $"{string.Join($" {AppStrings.Constants.and} ", missingFields)} {(missingFields.Count > 1 ? $"{AppStrings.Messages.areRequired}" : $"{AppStrings.Messages.isRequired}")} {AppStrings.Constants.Required}"
                );

            if (!ObjectId.TryParse(request.UserId, out _))
                throw new ArgumentException(AppStrings.Messages.UserNotFound);
            //  Get user by ID directly from repository
            var user = await _userRepository.GetUserByIdAsync(request.UserId!);
          

            if (user == null)
                throw new ArgumentException(AppStrings.Messages.UserNotFound);

            // Determine destination
            string destination = request.OtpType switch
            {
                var type when type == AppStrings.Constants.email => !string.IsNullOrWhiteSpace(
                    user.UserEmail
                )
                    ? user.UserEmail
                    : throw new ArgumentException(AppStrings.Messages.UserEmailNotAvailable),

                var type when type == AppStrings.Constants.phone => !string.IsNullOrWhiteSpace(
                    user.UserMobileNo
                )
                    ? user.UserMobileNo
                    : throw new ArgumentException(AppStrings.Messages.UserPhoneNotAvailable),

                _ => throw new ArgumentException(AppStrings.Messages.InvalidOtpType),
            };

            var latestOtp = await _otpRepository.GetLatestOtpByUserIdAndTypeAsync(
                user.UserId!,
                request.OtpType!
            );

            if (latestOtp != null && latestOtp.OtpEntryDate >= DateTime.UtcNow.AddMinutes(-15))
            {
                if (latestOtp.AttemptCount >= 3)
                    throw new ArgumentException(AppStrings.Messages.MaximumOtpAttempts);

                await SendOtp(request.OtpType!, destination, latestOtp.Otp);
                await _otpRepository.IncrementOtpAttemptAsync(latestOtp.Id!);
            }
            else
            {
                string newOtp = new Random().Next(100000, 999999).ToString();
                await SendOtp(request.OtpType!, destination, newOtp);

                await _otpRepository.SaveOtpAsync(
                    new OtpVerification
                    {
                        UserId = user.UserId,
                        Email =
                            request.OtpType == AppStrings.Constants.email ? user.UserEmail : null,
                        Phone =
                            request.OtpType == AppStrings.Constants.phone
                                ? user.UserMobileNo
                                : null,
                        Otp = newOtp,
                        OtpEntryDate = DateTime.UtcNow,
                        OtpUpdateDate=DateTime.UtcNow,
                        AttemptCount = 1,
                        OtpType = request.OtpType,
                    }
                );
            }

            return new
            {
                userId = user.UserId,
                type = request.OtpType,
                destination,
            };
        }

        private async Task SendOtp(string otpType, string destination, string otp) =>
            await (
                otpType == AppStrings.Constants.email
                    ? _otpService.SendOtpToEmail(destination, otp)
                    : _otpService.SendOtpToPhone(destination, otp)
            );
    }
}

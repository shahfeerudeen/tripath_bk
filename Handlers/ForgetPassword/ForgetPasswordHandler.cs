using MediatR;
using tripath.Commands;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;
using tripath.Services;
using tripath.Utils;

namespace tripath.Handlers
{
    public class ForgetPasswordHandler : IRequestHandler<ForgetPasswordCommand, object>
    {
        private readonly IMediator _mediator;
        private readonly IOtpRepository _otpRepository;
        private readonly IOtpService _otpService;
        private readonly ISmtpEmailService _smtpEmailService;

        public ForgetPasswordHandler(
            IMediator mediator,
            IOtpRepository otpRepository,
            IOtpService otpService,
            ISmtpEmailService smtpEmailService
        )
        {
            _mediator = mediator;
            _otpRepository = otpRepository;
            _otpService = otpService;
            _smtpEmailService = smtpEmailService;
        }

        public async Task<object> Handle(
            ForgetPasswordCommand request,
            CancellationToken cancellationToken
        )
        {
            // Validate username input
            _ = !string.IsNullOrWhiteSpace(request.UserName)
                ? request.UserName
                : throw new ArgumentException($"{AppStrings.Messages.UnameRequired}");

            // Get user by username
            var user = await _mediator.Send(new GetUserByUserNameQuery(request.UserName));

            _ = user ?? throw new ArgumentException($"{AppStrings.Messages.UserNotFound}");

            _ = !string.IsNullOrWhiteSpace(user.UserEmail)
                ? user.UserEmail
                : throw new ArgumentException($"{AppStrings.Messages.UserEmailNotAvailable}");

            _ = !string.IsNullOrWhiteSpace(user.UserMobileNo)
                ? user.UserMobileNo
                : throw new ArgumentException($"{AppStrings.Messages.UserPhoneNotAvailable}");

            // Generate OTPs
            var emailOtp = new Random().Next(100000, 999999).ToString();
            var phoneOtp = new Random().Next(100000, 999999).ToString();

            // Send OTPs
            await _otpService.SendOtpToEmail(user.UserEmail, emailOtp);
            await _otpService.SendOtpToPhone(user.UserMobileNo, phoneOtp);

            //Save the otp
            await _otpRepository.SaveOtpAsync(
                new OtpVerification
                {
                    UserId = user.UserId,
                    Email = user.UserEmail,
                    Phone = user.UserMobileNo,
                    Otp = $"{emailOtp}|{phoneOtp}", // You can split later if needed
                    OtpType = "both", // Indicate it's for both
                    OtpEntryDate = DateTime.UtcNow,
                }
            );

            // Return basic user info
            return new
            {
                userId = user.UserId,
                email = user.UserEmail,
                phone = user.UserMobileNo,
            };
        }
    }
}

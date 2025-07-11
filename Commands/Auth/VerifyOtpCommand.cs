using MediatR;
using tripath.ApiResponse;

namespace tripath.Commands
{
    public class VerifyOtpCommand : IRequest<string>
    {
        public string? UserId { get; set; }
        public string? Otp { get; set; }
         public string? otpType { get; set; }
    }
}

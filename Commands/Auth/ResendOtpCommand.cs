using MediatR;
using tripath.ApiResponse;

namespace tripath.Commands
{
    public class ResendOtpCommand : IRequest<object>
    {
        public string? UserId { get; set; }

        public string? OtpType { get; set; }
    }
}

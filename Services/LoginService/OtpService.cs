using System;
using System.Threading.Tasks;

namespace tripath.Services
{
    public class OtpService : IOtpService
    {
        private readonly ISmtpEmailService _smtpEmailService;

        public OtpService(ISmtpEmailService smtpEmailService)
        {
            _smtpEmailService = smtpEmailService;
        }

        public async Task SendOtpToEmail(string email, string otp)
        {
            await _smtpEmailService.SendOtpToEmail(email, otp); // call real email service
        }

        public Task SendOtpToPhone(string phone, string otp)
        {
            Console.WriteLine($"[DUMMY PHONE] OTP {otp} sent to: {phone}");
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tripath.Services
{
    public interface IOtpService
    {
        Task SendOtpToEmail(string email, string otp);
        Task SendOtpToPhone(string phone, string otp);
    }
}
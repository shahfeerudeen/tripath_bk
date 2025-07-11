using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tripath.Services
{
    public interface ISmtpEmailService
    {
        Task SendOtpToEmail(string email, string otp);
    }
}

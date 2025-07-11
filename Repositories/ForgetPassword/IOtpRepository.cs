using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tripath.Models;

namespace tripath.Repositories
{
    public interface IOtpRepository
    {
        Task SaveOtpAsync(OtpVerification otp);
        Task<OtpVerification?> GetLatestOtpAsync(string userId);
        Task IncrementOtpAttemptAsync(string otpId);
        Task<OtpVerification?> GetOtpByValueAsync(string otp);

        Task<OtpVerification?> GetLatestOtpByUserIdTypeAndOtpAsync(
            string userId,
            string otpType,
            string otp
        );
        Task<OtpVerification?> GetLatestOtpByUserIdAndTypeAsync(string userId, string otpType);
        Task<OtpVerification?> GetLatestOtpByUserIdAndOtpAsync(string userId, string otp);

        Task<OtpVerification?> GetOtpByUserIdOtpAndTypeAsync(string userId, string otp, string OtpType);

        //For session Mangement
        Task<bool> UpdateOtpSessionAsync(string otpId, string sessionId, bool isVerified, DateTime sessionExpiry);
        Task<OtpVerification?> GetOtpBySessionIdAsync(string sessionId);

    }
}

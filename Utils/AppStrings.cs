namespace tripath.Utils
{
    public class AppStrings
    {
        public static class Messages
        {
            public const string LoginSuccess = "Login successful";
            public const string somethingWentWrong = "Somthing Went Wrong";
            public const string LogOutSuccess = "Logout successful";
            public const string OrganizationNotFound = "Organization Not Found";
            public const string OrganizationList = "Organization List Fetched";
            public const string PasswordReset = "Password has been reset successfully.";
            public const string OtpSent = "OTP sent to email and phone";
            public const string InvalidUserIdCantResetPwd =
                "Invalid user ID or unable to reset password.";
            public const string PasswordChanged = "Password Has Been Changed";
            public const string ResetPasswordSomethingWrong =
                "Something went wrong while resetting the password.";
            public const string OtpSentTo = "OTP sent to ";
            public const string UnexpectedError = "An unexpected error occurred";
            public const string otpVerified = "OTP verified successfully";
            public const string OtpSomethingWrong = "Something went wrong while sending Otp";
            public const string UNamePwdOrgRequired =
                "Username, Password, and OrganizationId are required.";
            public const string UNmaePwdRequired = "Username and Password are required.";
            public const string UnameOrgIdRequired = "Username and OrganizationId are required.";
            public const string pwdOrgIdRequired = "Password and OrganizationId are required.";
            public const string UnameRequired = "Username is required.";
            public const string UIdRequired = "UserId is required.";
            public const string PwdRequired = "Password is required.";
            public const string OrgIdRequired = "OrganizationId is required.";
            public const string InvalidOrgId = "Invalid OrganizationId";
            public const string UserNotFound = "User not found";
            public const string UserNotActive = "User is not Active. Please contact Admin";
            public const string InvalidPwd = "Invalid password";
            public const string InvalidUserId = "Invalid UserId";
            public const string InvalidOtp = "Invalid OTP";
            public const string UserIdNotFound = "User ID not found in token.";
            public const string UserLogoutFail = "User logout failed";
            public const string UserEmailNotAvailable = "User email not available";
            public const string UserPhoneNotAvailable = "User phone number not available";
            public const string OtpTypeEmailOrPhone = "OtpType(email or phone)";
            public const string InvalidOtpType = "Invalid OtpType (must be 'email' or 'phone')";
            public const string MaximumOtpAttempts =
                "Maximum OTP resend attempts reached. Try again later.";
            public const string OtpExpired = "OTP expired";
            public const string RequestProcessed = "This request has already been processed";
            public const string RequestIdMissing = "Missing or empty X-Request-ID header.";
            public const string PwdNullEmpty = "Password cannot be null or empty.";
            public const string MissingSmtpUname = "Missing SMTP UserName";
            public const string MissingSmtpPwd = "Missing SMTP Password";
            public const string MissingSmtpHost = "Missing SMTP Host";
            public const string YourOtp = "Your OTP Code";
            public const string YourOtpIs = $"<p>Your OTP is </p>";
            public const string DontShareOtp = $"<p>Do not share it with anyone.</p>";
            public const string EmailSentTo = "Email sent to:";
            public const string isRequired = "is required";
            public const string areRequired = "are required";
            public const string InvalidOrExpiredSessionId = "Invalid or expired session ID.";

        }

        public static class Constants
        {
            public const string Login = "Login";
            public const string ForgetPassword = "Forget Password";
            public const string ResetPassword = "Reset Password";
            public const string verifyOtp = "Verify Otp";
            public const string ResendOtp = "Resend Otp";
            public const string Exception = "Exception";
            public const string UnKnown = "Unknown";
            public const string LoginHandler = "LoginHandler";
            public const string UserId = "UserId";
            public const string sub = "sub";
            public const string email = "email";
            public const string phone = "phone";
            public const string UserName = "UserName";
            public const string UserEmail = "UserEmail";
            public const string Required = "required";
            public const string AppLicationJson = "application/json";
            public const string RequestId = "RequestId";
            public const string RequestIdParam = " X-Request-ID";
            public const string Password = "Password";
            public const string Host = "Host";
            public const string Port = "Port";
            public const string EnableSsl = "EnableSsl";
            public const string SMTP = "SMTP";
            public const string Otp = "Otp";
            public const string OtpType = "OtpType";
            public const string and = "and";
            public const string newPassword = "New Password";
            public const string sessionId = "Session ID";
              public const string OrganisationId = "Organization ID";

        }

        public static class Status
        {
            public const string I = "I";
            public const string Y = "Y";
            public const string D = "D";
            public const string O = "O";
            public const string True = "true";
            public const string False = "false";
        }

        public static class Methods
        {
            public const string PUT = "PUT";
            public const string POST = "POST";
            public const string DELETE = "DELETE";
            public const string GET = "GET";
        }
    }
}

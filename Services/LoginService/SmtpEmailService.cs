using System.Net;
using System.Net.Mail;
using tripath.Utils;


namespace tripath.Services
{
    public class SmtpEmailService : ISmtpEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

     public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
    try
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");

        var fromEmail = smtpSettings[$"{AppStrings.Constants.UserName}"] ?? throw new InvalidOperationException($"{AppStrings.Messages.MissingSmtpUname}");
        var password = smtpSettings[$"{AppStrings.Constants.Password}"] ?? throw new InvalidOperationException($"{AppStrings.Messages.MissingSmtpPwd}");
        var host = smtpSettings[$"{AppStrings.Constants.Host}"] ?? throw new InvalidOperationException($"{AppStrings.Messages.MissingSmtpHost}");
        var port = int.Parse(smtpSettings[$"{AppStrings.Constants.Port}"] ?? "587");
        var enableSsl = bool.Parse(smtpSettings[$"{AppStrings.Constants.EnableSsl}"] ?? $"{AppStrings.Status.True}");

        var message = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(toEmail);

        using var smtp = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = enableSsl
        };

        await smtp.SendMailAsync(message);
        Console.WriteLine($"{AppStrings.Messages.EmailSentTo}: {toEmail}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{AppStrings.Constants.SMTP} {AppStrings.Constants.Exception} {ex.Message}");
            throw;
        }
    }

        public async Task SendOtpToEmail(string email, string otp)
        {
            var subject = $"{AppStrings.Messages.YourOtp}";
            var body = $"{AppStrings.Messages.YourOtpIs}<strong>{otp}</strong>.{AppStrings.Messages.DontShareOtp}</p>";
            await SendEmailAsync(email, subject, body);
        }
    }
}

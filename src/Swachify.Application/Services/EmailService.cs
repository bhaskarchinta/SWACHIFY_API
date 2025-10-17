using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Swachify.Application.Interfaces;

namespace Swachify.Application.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromAddress;
    private readonly string _senderName;

    public EmailService(IConfiguration configuration)
    {
        var smtpSection = configuration.GetSection("EmailSettings");

        var host = smtpSection["SmtpServer"];
        var port = int.Parse(smtpSection["Port"] ?? "587");
        var username = smtpSection["Username"];
        var password = smtpSection["Password"];
        _fromAddress = smtpSection["SenderEmail"];
        _senderName = smtpSection["SenderName"];
        var enableSsl = bool.Parse(smtpSection["EnableSsl"] ?? "true");

        _smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = enableSsl
        };
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_fromAddress, _senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mail.To.Add(to);

        await _smtpClient.SendMailAsync(mail);
    }
}

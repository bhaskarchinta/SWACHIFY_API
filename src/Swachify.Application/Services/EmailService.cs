using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailSettings = _config.GetSection("EmailSettings");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            emailSettings["SenderName"],
            emailSettings["SenderEmail"]
        ));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}

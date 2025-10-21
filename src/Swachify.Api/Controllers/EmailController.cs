using Microsoft.AspNetCore.Mvc;
using Swachify.Application.Interfaces;
using Swachify.Application.Models;

namespace Swachify.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("test")]
    public async Task<IActionResult> TestEmail([FromBody] EmailRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.To))
            return BadRequest("Recipient email is required.");


        var subject = "🌿 Welcome to Swachify!";

        var body = @"
<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Welcome to Swachify</title>
  </head>
  <body style=""margin:0; padding:0; background-color:#EDFDFE; font-family:Segoe UI, Arial, sans-serif;"">
    <table align=""center"" cellpadding=""0"" cellspacing=""0"" width=""600"" 
           style=""background-color:#ffffff; border-radius:10px; box-shadow:0 4px 15px rgba(0,0,0,0.08);"">
      
      <!-- Header -->
      <tr>
        <td align=""center"" bgcolor=""#16A34A"" 
            style=""padding:20px 0; border-top-left-radius:10px; border-top-right-radius:10px;"">
          <h1 style=""color:#ffffff; font-size:26px; margin:0;"">Welcome to Swachify!</h1>
        </td>
      </tr>

      <!-- Body -->
      <tr>
        <td style=""padding:30px 40px; color:#333333; font-size:16px; line-height:1.7;"">
          <p>Hello and welcome to <strong style=""color:#16A34A;"">Swachify</strong>!</p>

          <p>We’re excited to have you on board and look forward to working together toward a cleaner and smarter tomorrow.</p>

          <p>You’ll soon receive access details and next steps to help you get started.</p>

          <p>If you have any questions or need assistance, feel free to reach out to our team anytime at 
            <a href=""mailto:support@swachify.com"" style=""color:#16A34A; text-decoration:none;"">support@swachify.com</a>.
          </p>

          <p>Welcome once again to the <strong style=""color:#16A34A;"">Swachify</strong> family!</p>

          <p>Best regards,<br>
          <strong>Team Swachify</strong></p>
        </td>
      </tr>

      <!-- Footer -->
      <tr>
        <td align=""center"" bgcolor=""#F7FDFD"" style=""padding:20px; font-size:14px; color:#777777; border-bottom-left-radius:10px; border-bottom-right-radius:10px;"">
          &copy; 2025 Swachify. All rights reserved.
        </td>
      </tr>
<tr>
      <td align=""center"" bgcolor=""#F3F4F6"" style=""padding:15px; border-bottom-left-radius:10px; border-bottom-right-radius:10px;"">
        <a href=""https://swachify.com"" 
           style=""color:#2563EB; text-decoration:none; font-weight:bold;"">Visit Our Website</a>
      </td>
    </tr>

    </table>
  </body>
</html>


";


        await _emailService.SendEmailAsync(request.To, subject, body);

        return Ok($"Email sent successfully to {request.To}!");
    }
}

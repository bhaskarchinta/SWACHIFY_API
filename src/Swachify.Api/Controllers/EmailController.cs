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

        // Hardcoded subject and body
        var subject = "Welcome to Swachify!";
        var body = "<h1>Hello and welcome to Swachify!</h1><p>We’re glad to have you on board.</p>";

        await _emailService.SendEmailAsync(request.To, subject, body);

        return Ok($"Email sent successfully to {request.To}!");
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    // POST: /api/Email/test
    [HttpPost("test")]
    public async Task<IActionResult> TestEmail([FromBody] EmailRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.To))
            return BadRequest("Recipient email is required.");

        await _emailService.SendEmailAsync(request.To, "Test from Swachify", "<h1>Hello from Swachify!</h1>");
        return Ok($"Test email sent successfully to {request.To}!");
    }
}

public class EmailRequest
{
    public string To { get; set; }
}

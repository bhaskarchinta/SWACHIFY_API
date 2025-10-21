using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swachify.Application;
namespace Swachify.Api;

[ApiController]
[Route("api/[controller]")]
public class LoginController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> Login(string email, string password)
    {
        if (!(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password)))
        {
            var data = await authService.ValidateCredentialsAsync(email, password);
            if (data == null)
            {
                return Unauthorized("Invalid username or password");
            }
            else
            {
                return Ok(data);
            }
        }
        else
        {
            return Unauthorized("Invalid username or password");
        }
    }
    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassword(string email, string password, string confirmPassword)
    {
        var result = await authService.ForgotPasswordAsync(email, password, confirmPassword);

        if (result == "Password updated successfully.")
            return Ok(new { message = result });
        else
            return BadRequest(new { message = result });
    }

}

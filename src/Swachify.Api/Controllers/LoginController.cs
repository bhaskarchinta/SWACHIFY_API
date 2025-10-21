using Microsoft.AspNetCore.Mvc;
using Swachify.Application;
using Swachify.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
    {
        var result = await authService.ForgotPasswordAsync(
            request.Email,
            request.Password,
            request.ConfirmPassword
        );

        if (result == "Password updated successfully.")
            return Ok(new { message = result });

        return BadRequest(new { message = result });
    }

}

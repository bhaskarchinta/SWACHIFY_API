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
                return Ok("Login Successful");
            }
        }
        else
        {
            return Unauthorized("Invalid username or password");
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swachify.Application;
namespace Swachify.Api;

[ApiController]
[Route("api/[controller]")]
public class LoginController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public ActionResult Login(string email, string password)
    {
        if (!(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password)))
        {
            var data = authService.ValidateCredentialsAsync(email, password);
            return Ok("Login Successful");
        }
        else
        {
            return Unauthorized("Invalid username or password");
        }
    }
}

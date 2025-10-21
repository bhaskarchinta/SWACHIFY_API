using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swachify.Application;
using Swachify.Application.DTOs;
namespace Swachify.Api;

[ApiController]
[Route("api/[controller]")]
public class LoginController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> Login(loginDtos req)
    {
        if (!(string.IsNullOrEmpty(req.email) && string.IsNullOrEmpty(req.password)))
        {
            var data = await authService.ValidateCredentialsAsync(req.email, req.password);
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
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swachify.Application;

namespace Swachify.Api;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> UserRegister(UserCommandDto userCommandDto)
    {
        var result = await userService.CreateUserAsync(userCommandDto);
        if (result == null) return Forbid();
        return Ok(result);
    }

}
using Microsoft.AspNetCore.Mvc;
using Swachify.Application;

namespace Swachify.Api;

[ApiController]
[Route("api/[controller]")]
public class MasterController(IMasterService masterService) : ControllerBase
{
    [HttpGet("getallservices")]
    public async Task<IActionResult> GetAllServices()
    {
        return Ok(await masterService.GetAllServicesAsync());
    }

    [HttpGet("getalllocations")]
    public async Task<IActionResult> GetAllLocations()
    {
        return Ok(await masterService.GetAllLocationsAsync());
    }
    
     [HttpGet("getallroles")]
    public async Task<IActionResult> GetAllRoles()
    {
        return Ok(await masterService.GetAllRolesAsync());
    }
}

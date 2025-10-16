using Microsoft.AspNetCore.Mvc;
using Swachify.Application;

namespace Swachify.Api;

[ApiController]
[Route("api/[controller]")]
public class ServiceController(ICleaningService cleaningService) : ControllerBase
{
   
}

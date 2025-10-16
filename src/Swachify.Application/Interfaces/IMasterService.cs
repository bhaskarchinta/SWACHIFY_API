using Swachify.Infrastructure.Models;

namespace Swachify.Application;

public interface IMasterService
{
    Task<List<master_service>> GetAllServicesAsync();
    Task<List<master_location>> GetAllLocationsAsync();
    Task<List<master_role>> GetAllRolesAsync();
}
using Swachify.Infrastructure.Models;

namespace Swachify.Application;

public interface IUserService
{
    Task<long> CreateUserAsync(UserCommandDto cmd, CancellationToken ct = default);

    Task<List<user_registration>> GetAllUsersAsync();

    Task<List<user_registration>> GetAllUsersByDept(long deptId);
    Task<user_registration> GetUserByID(long id);

    Task<long> CreateEmployeAsync(EmpCommandDto cmd, CancellationToken ct = default);
}
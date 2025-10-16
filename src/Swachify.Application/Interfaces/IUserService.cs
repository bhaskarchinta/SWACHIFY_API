namespace Swachify.Application;

public interface IUserService
{
    Task<long> CreateUserAsync(UserCommandDto cmd, CancellationToken ct = default);
}
using Swachify.Infrastructure.Models;

namespace Swachify.Application;

public interface IAuthService
{
    Task<user_auth?> ValidateCredentialsAsync(string email, string password, CancellationToken ct = default);
}

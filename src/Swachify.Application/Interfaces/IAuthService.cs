using Swachify.Infrastructure.Models;

namespace Swachify.Application;

public interface IAuthService
{
    Task<user_registration?> ValidateCredentialsAsync(string email, string password, CancellationToken ct = default);
}

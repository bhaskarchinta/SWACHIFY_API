using Microsoft.AspNetCore.Identity;
using Swachify.Infrastructure.Models;
using Swachify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Swachify.Application;

public class AuthService(MyDbContext db, IPasswordHasher hasher) : IAuthService
{
    public async Task<user_registration?> ValidateCredentialsAsync(string email, string password, CancellationToken ct = default)
    {
        var user_auth = await db.user_auths.FirstOrDefaultAsync(u => u.login_name == email);
        if (user_auth is null) return null;
        var user_reg = await db.user_registrations.FirstOrDefaultAsync(u => u.email == email);
        if (user_reg != null)
        {
            user_reg.password = "*********";
            user_reg.user_authusers = new List<user_auth>();
        }
        return hasher.Verify(password, user_auth.password) ? user_reg : null;
    }
}

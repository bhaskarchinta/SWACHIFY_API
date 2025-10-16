using Microsoft.AspNetCore.Identity;
using Swachify.Infrastructure.Models;
using Swachify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Swachify.Application;

public class AuthService(MyDbContext db,IPasswordHasher hasher) : IAuthService
{
    public async Task<user_auth?> ValidateCredentialsAsync(string email, string password, CancellationToken ct = default)
    {
       var user_auth = await db.user_auths.FirstOrDefaultAsync(u => u.login_name == email);
       if (user_auth is null) return null;
       return hasher.Verify(password, user_auth.password) ? user_auth : null;
    }
}

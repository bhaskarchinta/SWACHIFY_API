using Microsoft.AspNetCore.Identity;
using Swachify.Infrastructure.Models;
using Swachify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Swachify.Application;

public class AuthService(MyDbContext db, IPasswordHasher hasher) : IAuthService
{
    public async Task<user_registration?> ValidateCredentialsAsync(string email, string password, CancellationToken ct = default)
    {
        var user_auth = await db.user_auths.FirstOrDefaultAsync(u => u.email == email);
        if (user_auth is null) return null;
        var user_reg = await db.user_registrations.FirstOrDefaultAsync(u => u.email == email);
        if (user_reg != null)
        {
            user_reg.user_authusers = new List<user_auth>();
        }
        return hasher.Verify(password, user_auth.password) ? user_reg : null;
    }
    public async Task<string> ForgotPasswordAsync(string email, string newPassword, string confirmPassword, CancellationToken ct = default)
    {
       
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            return "Email and passwords are required.";

        if (newPassword != confirmPassword)
            return "Password and Confirm Password do not match.";

        var userAuth = await db.user_auths.FirstOrDefaultAsync(u => u.email == email, ct);
        if (userAuth == null)
            return "Email not found. Please check your email or register first.";

       
        userAuth.password = hasher.Hash(newPassword);
        userAuth.modified_date = DateTime.Now;

       
        db.user_auths.Update(userAuth);
        await db.SaveChangesAsync(ct);

        return "Password updated successfully.";
    }
}

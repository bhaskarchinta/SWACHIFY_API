using Microsoft.EntityFrameworkCore;
using Swachify.Infrastructure.Data;
using Swachify.Infrastructure.Models;

namespace Swachify.Application;

public class UserService(MyDbContext db, IPasswordHasher hasher) : IUserService
{

    public async Task<long> CreateUserAsync(UserCommandDto cmd, CancellationToken ct = default)
    {
        if (await db.user_registrations.AnyAsync(u => u.email == cmd.email, ct))
            throw new InvalidOperationException("Email exists");
        long id = await db.user_registrations.MaxAsync(u => (long?)u.id) ?? 0L;

        var user = new user_registration
        {
            id = id + 1,
            email = cmd.email,
            first_name = cmd.first_name,
            last_name = cmd.last_name,
            mobile = cmd.mobile,
        };
        await db.user_registrations.AddAsync(user);
        long user_auth_id = await db.user_auths.MaxAsync(u => (long?)u.id) ?? 0L;
        var user_auth = new user_auth
        {
            id = user_auth_id + 1,
            user_id = user.id,
            email = cmd.email,
            password = hasher.Hash(cmd.password)
        };
        await db.user_auths.AddAsync(user_auth);
        await db.SaveChangesAsync(ct);
        return user.id;
    }

    public async Task<List<user_registration>> GetAllUsersAsync()
    {
        var result = await db.user_registrations?.ToListAsync();
        return result;
    }

    public async Task<List<user_registration>> GetAllUsersByDept(long deptId)
    {
        return await db.user_registrations.Where(d => d.dept_id == deptId)?.ToListAsync();
    }

    public async Task<user_registration> GetUserByID(long id)
    {
        var user = await db.user_registrations?.FirstOrDefaultAsync(d => d.id == id);
        return user;
    }

    public async Task<long> CreateEmployeAsync(EmpCommandDto cmd, CancellationToken ct = default)
    {
        if (await db.user_registrations.AnyAsync(u => u.email == cmd.email, ct))
            throw new InvalidOperationException("Email exists");
        long id = await db.user_registrations.MaxAsync(u => (long?)u.id) ?? 0L;

        var user = new user_registration
        {
            id = id + 1,
            email = cmd.email,
            first_name = cmd.first_name,
            last_name = cmd.last_name,
            mobile = cmd.mobile,
        };
        await db.user_registrations.AddAsync(user);
        long user_auth_id = await db.user_auths.MaxAsync(u => (long?)u.id) ?? 0L;
        var user_auth = new user_auth
        {
            id = user_auth_id + 1,
            user_id = user.id,
            email = cmd.email,
            password = hasher.Hash("Indian@123")
        };
        await db.user_auths.AddAsync(user_auth);
        await db.SaveChangesAsync(ct);
        return user.id;
    }
}
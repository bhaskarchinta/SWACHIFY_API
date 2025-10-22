using Microsoft.EntityFrameworkCore;
using Npgsql;
using Swachify.Application.DTOs;
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
            role_id = cmd.role_id
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

    public async Task<List<AllUserDtos>> GetAllUsersAsync()
    {
        var users = await db.user_registrations.ToListAsync();
        var userIds = users.Select(u => u.id).ToList();

        var userDeptPairs = await (
            from ud in db.user_departments
            where userIds.Contains(ud.user_id)
            join md in db.master_departments on ud.dept_id equals md.id
            select new { ud.user_id, md.department_name }
        ).ToListAsync();

        var grouped = userDeptPairs
            .GroupBy(x => x.user_id)
            .ToDictionary(g => g.Key, g => g.Select(x => x.department_name).ToList());

        var allUsers = users.Select(u => new AllUserDtos
        {
            id = u.id,
            age = u.age,
            dept_id = u.dept_id,
            email = u.email,
            first_name = u.first_name,
            gender_id = u.gender_id,
            is_active = u.is_active,
            location_id = u.location_id,
            depts = grouped.TryGetValue(u.id, out var list) ? list : new List<string>()
        }).ToList();
        return allUsers ?? new List<AllUserDtos>();
    }

    public async Task<List<user_registration>> GetAllUsersByDept(long deptId)
    {
        return await db.user_registrations.Where(d => d.dept_id == deptId)?.ToListAsync();
    }

    public async Task<AllUserDtos> GetUserByID(long id)
    {
        var user = await db.user_registrations?.FirstOrDefaultAsync(d => d.id == id);
        if (user == null) return new AllUserDtos();
        var userDept = await (
            from ud in db.user_departments
            where ud.user_id == user.id
            join md in db.master_departments on ud.dept_id equals md.id
            select md.department_name).ToListAsync();

        var userResult = new AllUserDtos
        {
            id = user.id,
            age = user.age,
            dept_id = user.dept_id,
            email = user.email,
            first_name = user.first_name,
            gender_id = user.gender_id,
            is_active = user.is_active,
            depts = userDept,
            location_id = user.location_id
        };

        return userResult;
    }

public async Task<long> CreateEmployeAsync(EmpCommandDto cmd, CancellationToken ct = default)
{
    if (cmd == null) throw new ArgumentNullException(nameof(cmd));
    ct.ThrowIfCancellationRequested();

    if (string.IsNullOrWhiteSpace(cmd.email))
        throw new ArgumentException("Email is required", nameof(cmd.email));

    if (await db.user_registrations.AnyAsync(u => u.email == cmd.email, ct))
        throw new InvalidOperationException("Email exists");
        long userid = await db.user_registrations.MaxAsync(u => (long?)u.id) ?? 0L;
        userid = userid + 1;
    var user = new user_registration
    {
        id=userid,
        email = cmd.email,
        first_name = cmd.first_name,
        last_name = cmd.last_name,
        mobile = cmd.mobile,
        role_id = 3,
        location_id = cmd.location_id,
    };

    await using var tx = await db.Database.BeginTransactionAsync(ct);

    try
    {
        await db.user_registrations.AddAsync(user, ct);
        await db.SaveChangesAsync(ct); 

        if (cmd.dept_id != null && cmd.dept_id.Any())
        {
            var userDeptList = new List<user_department>(cmd.dept_id.Count);
            foreach (var d in cmd.dept_id)
            {
                userDeptList.Add(new user_department
                {
                    user_id = userid,
                    dept_id = d
                });
            }

            if (userDeptList.Count > 0)
            {
                await db.user_departments.AddRangeAsync(userDeptList, ct);
            }
        }

        var userAuth = new user_auth
        {
            id=userid,
            user_id = userid,
            email = cmd.email,
            password = hasher.Hash(cmd.email) 
        };

        await db.user_auths.AddAsync(userAuth, ct);

        await db.SaveChangesAsync();

        await tx.CommitAsync(ct);

        return user.id;
    }
    catch (DbUpdateException dbEx)
    {
        if (dbEx.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
        {
            throw new InvalidOperationException("Email already exists (unique constraint)", dbEx);
        }

        throw;
    }
}


    public async Task<bool> AssignEmployee(long id, long user_id)
    {
        var existing = await db.service_bookings.FirstOrDefaultAsync(b => b.id == id);
        if (existing == null) return false;
        existing.status_id = 2;
        existing.assign_to = user_id;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserAsync(long id, EmpCommandDto cmd)
    {
        var existing = await db.user_registrations.FirstOrDefaultAsync(b => b.id == id);
        if (existing == null) return false;

        existing.email = cmd.email;
        existing.first_name = cmd.first_name;
        existing.last_name = cmd.last_name;
        existing.mobile = cmd.mobile;
        existing.role_id = cmd.role_id;
        existing.location_id = cmd.location_id;
        await db.SaveChangesAsync();
        return true;
    }

}
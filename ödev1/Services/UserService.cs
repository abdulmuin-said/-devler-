using KeyloggerTespitSistemi.Data;
using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users.Include(item => item.Role).OrderBy(item => item.FullName).ToListAsync();
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await _dbContext.Roles.OrderBy(item => item.Name).ToListAsync();
    }

    public async Task CreateAsync(UserFormViewModel model)
    {
        var email = model.Email.Trim().ToLowerInvariant();
        var exists = await _dbContext.Users.AnyAsync(item => item.Email == email);
        if (exists)
        {
            throw new InvalidOperationException("Bu e-posta adresi zaten kullanılıyor.");
        }

        var user = new User
        {
            FullName = model.FullName.Trim(),
            Email = email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            RoleId = model.RoleId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ToggleAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user is null || user.Id == 1)
        {
            return;
        }

        user.IsActive = !user.IsActive;
        await _dbContext.SaveChangesAsync();
    }
}

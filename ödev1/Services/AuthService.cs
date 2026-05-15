using KeyloggerTespitSistemi.Data;
using KeyloggerTespitSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await _dbContext.Users
            .Include(item => item.Role)
            .FirstOrDefaultAsync(item => item.Email == email && item.IsActive);

        if (user is null || !PasswordHasher.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }
}

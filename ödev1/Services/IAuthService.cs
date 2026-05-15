using KeyloggerTespitSistemi.Models;

namespace KeyloggerTespitSistemi.Services;

public interface IAuthService
{
    Task<User?> ValidateCredentialsAsync(string email, string password);
}

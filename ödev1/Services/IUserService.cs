using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.ViewModels;

namespace KeyloggerTespitSistemi.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<List<Role>> GetRolesAsync();
    Task CreateAsync(UserFormViewModel model);
    Task ToggleAsync(int id);
}

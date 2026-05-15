using KeyloggerTespitSistemi.ViewModels;

namespace KeyloggerTespitSistemi.Services;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync();
}

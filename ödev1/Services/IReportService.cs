using KeyloggerTespitSistemi.ViewModels;

namespace KeyloggerTespitSistemi.Services;

public interface IReportService
{
    Task<ReportsViewModel> GetReportsAsync();
}

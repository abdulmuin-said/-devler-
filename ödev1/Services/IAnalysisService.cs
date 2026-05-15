using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.ViewModels;

namespace KeyloggerTespitSistemi.Services;

public interface IAnalysisService
{
    Task<AnalysisResult> CreateAnalysisAsync(ProcessReportCreateViewModel model, int userId);
    Task<List<ProcessReport>> GetReportsAsync();
    Task<AnalysisDetailViewModel?> GetDetailAsync(int id);
}

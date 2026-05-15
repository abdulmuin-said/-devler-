using KeyloggerTespitSistemi.Data;
using KeyloggerTespitSistemi.Models.Enums;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _dbContext;

    public DashboardService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        return new DashboardViewModel
        {
            TotalAnalyses = await _dbContext.AnalysisResults.CountAsync(),
            LowRiskCount = await _dbContext.AnalysisResults.CountAsync(item => item.RiskLevel == RiskLevel.Low),
            MediumRiskCount = await _dbContext.AnalysisResults.CountAsync(item => item.RiskLevel == RiskLevel.Medium),
            HighRiskCount = await _dbContext.AnalysisResults.CountAsync(item => item.RiskLevel == RiskLevel.High),
            RecentReports = await _dbContext.ProcessReports
                .Include(item => item.AnalysisResult)
                .Include(item => item.User)
                .OrderByDescending(item => item.CreatedAt)
                .Take(5)
                .ToListAsync()
        };
    }
}

using KeyloggerTespitSistemi.Data;
using KeyloggerTespitSistemi.Models.Enums;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _dbContext;

    public ReportService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReportsViewModel> GetReportsAsync()
    {
        var mostTriggeredRules = await _dbContext.TriggeredRules
            .Include(item => item.DetectionRule)
            .GroupBy(item => item.DetectionRule!.Name)
            .Select(group => new TriggeredRuleReportItem
            {
                RuleName = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(item => item.Count)
            .Take(10)
            .ToListAsync();

        return new ReportsViewModel
        {
            TotalAnalyses = await _dbContext.AnalysisResults.CountAsync(),
            LowRiskCount = await _dbContext.AnalysisResults.CountAsync(item => item.RiskLevel == RiskLevel.Low),
            MediumRiskCount = await _dbContext.AnalysisResults.CountAsync(item => item.RiskLevel == RiskLevel.Medium),
            HighRiskCount = await _dbContext.AnalysisResults.CountAsync(item => item.RiskLevel == RiskLevel.High),
            MostTriggeredRules = mostTriggeredRules
        };
    }
}

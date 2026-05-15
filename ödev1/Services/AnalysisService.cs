using KeyloggerTespitSistemi.Data;
using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.Models.Enums;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Services;

public class AnalysisService : IAnalysisService
{
    private readonly ApplicationDbContext _dbContext;

    public AnalysisService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AnalysisResult> CreateAnalysisAsync(ProcessReportCreateViewModel model, int userId)
    {
        var report = new ProcessReport
        {
            UserId = userId,
            ProcessName = model.ProcessName.Trim(),
            FilePath = model.FilePath.Trim(),
            Publisher = model.Publisher?.Trim(),
            RunsAtStartup = model.RunsAtStartup,
            RunsInBackground = model.RunsInBackground,
            KeyboardAccessSuspicion = model.KeyboardAccessSuspicion,
            HasNetworkConnection = model.HasNetworkConnection,
            RunsFromTempOrAppData = model.RunsFromTempOrAppData,
            FakeSystemFileName = model.FakeSystemFileName,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow
        };

        var rules = await _dbContext.DetectionRules
            .Where(rule => rule.IsActive)
            .OrderBy(rule => rule.Id)
            .ToListAsync();

        var triggeredRules = new List<TriggeredRule>();
        var score = 0;

        foreach (var rule in rules)
        {
            if (!IsRuleTriggered(rule.RuleType, report))
            {
                continue;
            }

            score += rule.RiskPoint;
            triggeredRules.Add(new TriggeredRule
            {
                DetectionRuleId = rule.Id,
                AddedScore = rule.RiskPoint,
                Reason = BuildReason(rule.RuleType)
            });
        }

        score = Math.Min(score, 100);
        var riskLevel = CalculateRiskLevel(score);

        var result = new AnalysisResult
        {
            ProcessReport = report,
            TotalScore = score,
            RiskLevel = riskLevel,
            Summary = BuildSummary(score, riskLevel, triggeredRules.Count),
            CreatedAt = DateTime.UtcNow,
            TriggeredRules = triggeredRules
        };

        _dbContext.AnalysisResults.Add(result);
        await _dbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<ProcessReport>> GetReportsAsync()
    {
        return await _dbContext.ProcessReports
            .Include(report => report.User)
            .Include(report => report.AnalysisResult)
            .OrderByDescending(report => report.CreatedAt)
            .ToListAsync();
    }

    public async Task<AnalysisDetailViewModel?> GetDetailAsync(int id)
    {
        var report = await _dbContext.ProcessReports
            .Include(item => item.User)
            .Include(item => item.AnalysisResult)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (report?.AnalysisResult is null)
        {
            return null;
        }

        var triggeredRules = await _dbContext.TriggeredRules
            .Include(item => item.DetectionRule)
            .Where(item => item.AnalysisResultId == report.AnalysisResult.Id)
            .ToListAsync();

        var recommendations = await _dbContext.Recommendations
            .Where(item => item.IsActive && item.RiskLevel == report.AnalysisResult.RiskLevel)
            .ToListAsync();

        return new AnalysisDetailViewModel
        {
            Report = report,
            Result = report.AnalysisResult,
            TriggeredRules = triggeredRules,
            Recommendations = recommendations
        };
    }

    private static bool IsRuleTriggered(DetectionRuleType type, ProcessReport report)
    {
        return type switch
        {
            DetectionRuleType.UnknownPublisher => string.IsNullOrWhiteSpace(report.Publisher) || report.Publisher.Equals("unknown", StringComparison.OrdinalIgnoreCase) || report.Publisher.Equals("bilinmeyen", StringComparison.OrdinalIgnoreCase),
            DetectionRuleType.RunsAtStartup => report.RunsAtStartup,
            DetectionRuleType.RunsFromTempOrAppData => report.RunsFromTempOrAppData,
            DetectionRuleType.KeyboardAccessSuspicion => report.KeyboardAccessSuspicion,
            DetectionRuleType.SuspiciousNetworkConnection => report.HasNetworkConnection,
            DetectionRuleType.FakeSystemFileName => report.FakeSystemFileName,
            DetectionRuleType.RunsInBackground => report.RunsInBackground,
            _ => false
        };
    }

    private static RiskLevel CalculateRiskLevel(int score)
    {
        if (score <= 30)
        {
            return RiskLevel.Low;
        }

        return score <= 60 ? RiskLevel.Medium : RiskLevel.High;
    }

    private static string BuildReason(DetectionRuleType type)
    {
        return type switch
        {
            DetectionRuleType.UnknownPublisher => "Yayıncı bilgisi bilinmeyen olarak değerlendirildi.",
            DetectionRuleType.RunsAtStartup => "Başlangıçta otomatik çalıştığı bildirildi.",
            DetectionRuleType.RunsFromTempOrAppData => "Temp veya AppData konumundan çalıştığı bildirildi.",
            DetectionRuleType.KeyboardAccessSuspicion => "Klavye erişimi şüphesi manuel olarak işaretlendi.",
            DetectionRuleType.SuspiciousNetworkConnection => "Ağ bağlantısı bulunduğu bildirildi.",
            DetectionRuleType.FakeSystemFileName => "Sahte sistem dosyası adı şüphesi işaretlendi.",
            DetectionRuleType.RunsInBackground => "Arka planda çalışma davranışı bildirildi.",
            _ => "Kural tetiklendi."
        };
    }

    private static string BuildSummary(int score, RiskLevel riskLevel, int triggeredCount)
    {
        var riskName = riskLevel switch
        {
            RiskLevel.Low => "Düşük Risk",
            RiskLevel.Medium => "Orta Risk",
            RiskLevel.High => "Yüksek Risk",
            _ => "Bilinmiyor"
        };

        return $"Toplam {triggeredCount} kural tetiklendi. Risk puanı {score}, seviye {riskName}.";
    }
}

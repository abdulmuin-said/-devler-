using KeyloggerTespitSistemi.Models.Enums;

namespace KeyloggerTespitSistemi.Models;

public class AnalysisResult
{
    public int Id { get; set; }

    public int ProcessReportId { get; set; }

    public ProcessReport? ProcessReport { get; set; }

    public int TotalScore { get; set; }

    public RiskLevel RiskLevel { get; set; }

    public string Summary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TriggeredRule> TriggeredRules { get; set; } = new List<TriggeredRule>();
}

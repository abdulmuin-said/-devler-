namespace KeyloggerTespitSistemi.Models;

public class TriggeredRule
{
    public int Id { get; set; }

    public int AnalysisResultId { get; set; }

    public AnalysisResult? AnalysisResult { get; set; }

    public int DetectionRuleId { get; set; }

    public DetectionRule? DetectionRule { get; set; }

    public int AddedScore { get; set; }

    public string Reason { get; set; } = string.Empty;
}

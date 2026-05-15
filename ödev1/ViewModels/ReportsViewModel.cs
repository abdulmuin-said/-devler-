namespace KeyloggerTespitSistemi.ViewModels;

public class ReportsViewModel
{
    public int TotalAnalyses { get; set; }
    public int LowRiskCount { get; set; }
    public int MediumRiskCount { get; set; }
    public int HighRiskCount { get; set; }
    public List<TriggeredRuleReportItem> MostTriggeredRules { get; set; } = new();
}

public class TriggeredRuleReportItem
{
    public string RuleName { get; set; } = string.Empty;
    public int Count { get; set; }
}

using KeyloggerTespitSistemi.Models;

namespace KeyloggerTespitSistemi.ViewModels;

public class DashboardViewModel
{
    public int TotalAnalyses { get; set; }
    public int LowRiskCount { get; set; }
    public int MediumRiskCount { get; set; }
    public int HighRiskCount { get; set; }
    public List<ProcessReport> RecentReports { get; set; } = new();
}

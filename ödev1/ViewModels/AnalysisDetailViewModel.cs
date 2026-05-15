using KeyloggerTespitSistemi.Models;

namespace KeyloggerTespitSistemi.ViewModels;

public class AnalysisDetailViewModel
{
    public ProcessReport Report { get; set; } = new();
    public AnalysisResult Result { get; set; } = new();
    public List<TriggeredRule> TriggeredRules { get; set; } = new();
    public List<Recommendation> Recommendations { get; set; } = new();
}

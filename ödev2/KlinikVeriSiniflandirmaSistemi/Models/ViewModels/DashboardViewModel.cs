namespace KlinikVeriSiniflandirmaSistemi.Models.ViewModels;

public class DashboardViewModel
{
    public int TotalPatients { get; set; }

    public int TotalClinicalRecords { get; set; }

    public int NormalCount { get; set; }

    public int RiskyCount { get; set; }

    public int EmergencyCount { get; set; }

    public List<ClassStatisticViewModel> ClassStatistics { get; set; } = new();

    public List<ClinicalRecord> LatestClinicalRecords { get; set; } = new();
}

public class ClassStatisticViewModel
{
    public string ClassName { get; set; } = string.Empty;

    public string RiskLevel { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;

    public int Count { get; set; }
}

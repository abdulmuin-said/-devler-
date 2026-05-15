namespace KeyloggerTespitSistemi.Models.Enums;

public static class RiskLevelExtensions
{
    public static string GetDisplayName(this RiskLevel riskLevel)
    {
        return riskLevel switch
        {
            RiskLevel.Low => "Düşük Risk",
            RiskLevel.Medium => "Orta Risk",
            RiskLevel.High => "Yüksek Risk",
            _ => "Bilinmiyor"
        };
    }

    public static string GetBadgeClass(this RiskLevel riskLevel)
    {
        return riskLevel switch
        {
            RiskLevel.Low => "text-bg-success",
            RiskLevel.Medium => "text-bg-warning",
            RiskLevel.High => "text-bg-danger",
            _ => "text-bg-secondary"
        };
    }
}

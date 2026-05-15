using KlinikVeriSiniflandirmaSistemi.Models;

namespace KlinikVeriSiniflandirmaSistemi.Services;

public class ClassificationService : IClassificationService
{
    public string Classify(ClinicalRecord record)
    {
        var abnormalCount = 0;

        if (record.Pulse is < 50 or > 120)
        {
            return "Acil";
        }

        if (record.BloodSugar > 180)
        {
            abnormalCount++;
        }

        if (record.BodyTemperature > 38)
        {
            abnormalCount++;
        }

        if (record.SystolicBloodPressure >= 140 || record.DiastolicBloodPressure >= 90)
        {
            abnormalCount++;
        }

        if (record.Cholesterol > 240)
        {
            abnormalCount++;
        }

        if (abnormalCount >= 2)
        {
            return "Acil";
        }

        var text = $"{record.Diagnosis} {record.Notes}".ToLowerInvariant();
        if (text.Contains("kronik") || text.Contains("takip"))
        {
            return "Kronik Takip Gerekli";
        }

        return abnormalCount == 1 ? "Riskli" : "Normal";
    }
}

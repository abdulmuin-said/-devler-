using KlinikVeriSiniflandirmaSistemi.Models;

namespace KlinikVeriSiniflandirmaSistemi.Services;

public interface IClassificationService
{
    string Classify(ClinicalRecord record);
}

using System.ComponentModel.DataAnnotations;

namespace KlinikVeriSiniflandirmaSistemi.Models;

public class ClinicalClass
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Sınıf adı zorunludur.")]
    [StringLength(100)]
    [Display(Name = "Sınıf Adı")]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Risk seviyesi zorunludur.")]
    [StringLength(100)]
    [Display(Name = "Risk Seviyesi")]
    public string RiskLevel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Renk kodu zorunludur.")]
    [StringLength(20)]
    [Display(Name = "Renk Kodu")]
    public string ColorCode { get; set; } = string.Empty;

    public ICollection<ClinicalRecord> ClinicalRecords { get; set; } = new List<ClinicalRecord>();
}

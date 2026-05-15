using System.ComponentModel.DataAnnotations;
using KeyloggerTespitSistemi.Models.Enums;

namespace KeyloggerTespitSistemi.ViewModels;

public class DetectionRuleFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    [Display(Name = "Kural Adı")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    [Display(Name = "Açıklama")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Kural Tipi")]
    public DetectionRuleType RuleType { get; set; }

    [Range(1, 100)]
    [Display(Name = "Risk Puanı")]
    public int RiskPoint { get; set; } = 10;

    [Display(Name = "Aktif")]
    public bool IsActive { get; set; } = true;
}

using System.ComponentModel.DataAnnotations;
using KeyloggerTespitSistemi.Models.Enums;

namespace KeyloggerTespitSistemi.Models;

public class Recommendation
{
    public int Id { get; set; }

    public RiskLevel RiskLevel { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(700)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}

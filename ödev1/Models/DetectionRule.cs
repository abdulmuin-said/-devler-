using System.ComponentModel.DataAnnotations;
using KeyloggerTespitSistemi.Models.Enums;

namespace KeyloggerTespitSistemi.Models;

public class DetectionRule
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DetectionRuleType RuleType { get; set; }

    [Range(1, 100)]
    public int RiskPoint { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<TriggeredRule> TriggeredRules { get; set; } = new List<TriggeredRule>();
}

using System.ComponentModel.DataAnnotations;

namespace KeyloggerTespitSistemi.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(128)]
    public string PasswordHash { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public Role? Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProcessReport> ProcessReports { get; set; } = new List<ProcessReport>();

    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}

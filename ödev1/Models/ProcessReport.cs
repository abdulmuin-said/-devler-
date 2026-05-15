using System.ComponentModel.DataAnnotations;

namespace KeyloggerTespitSistemi.Models;

public class ProcessReport
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

    [Required(ErrorMessage = "Process adı zorunludur.")]
    [StringLength(120)]
    public string ProcessName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dosya yolu zorunludur.")]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;

    [StringLength(150)]
    public string? Publisher { get; set; }

    public bool RunsAtStartup { get; set; }

    public bool RunsInBackground { get; set; }

    public bool KeyboardAccessSuspicion { get; set; }

    public bool HasNetworkConnection { get; set; }

    public bool RunsFromTempOrAppData { get; set; }

    public bool FakeSystemFileName { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public AnalysisResult? AnalysisResult { get; set; }
}

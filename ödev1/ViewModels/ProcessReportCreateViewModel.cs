using System.ComponentModel.DataAnnotations;

namespace KeyloggerTespitSistemi.ViewModels;

public class ProcessReportCreateViewModel
{
    [Required(ErrorMessage = "Process adı zorunludur.")]
    [StringLength(120)]
    [Display(Name = "Process Adı")]
    public string ProcessName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dosya yolu zorunludur.")]
    [StringLength(500)]
    [Display(Name = "Dosya Yolu")]
    public string FilePath { get; set; } = string.Empty;

    [StringLength(150)]
    [Display(Name = "Yayıncı")]
    public string? Publisher { get; set; }

    [Display(Name = "Başlangıçta çalışıyor")]
    public bool RunsAtStartup { get; set; }

    [Display(Name = "Arka planda çalışıyor")]
    public bool RunsInBackground { get; set; }

    [Display(Name = "Klavye erişimi şüphesi")]
    public bool KeyboardAccessSuspicion { get; set; }

    [Display(Name = "Ağ bağlantısı var")]
    public bool HasNetworkConnection { get; set; }

    [Display(Name = "Temp/AppData konumundan çalışıyor")]
    public bool RunsFromTempOrAppData { get; set; }

    [Display(Name = "Sahte sistem dosyası adı")]
    public bool FakeSystemFileName { get; set; }

    [StringLength(1000)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }
}

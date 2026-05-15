using System.ComponentModel.DataAnnotations;

namespace KlinikVeriSiniflandirmaSistemi.Models;

public class ClinicalRecord
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Hasta seçimi zorunludur.")]
    [Display(Name = "Hasta")]
    public int PatientId { get; set; }

    public Patient? Patient { get; set; }

    [Display(Name = "Sınıf")]
    public int ClinicalClassId { get; set; }

    public ClinicalClass? ClinicalClass { get; set; }

    [Required(ErrorMessage = "Şikayet zorunludur.")]
    [StringLength(500)]
    [Display(Name = "Şikayet")]
    public string Complaint { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tanı zorunludur.")]
    [StringLength(300)]
    [Display(Name = "Tanı")]
    public string Diagnosis { get; set; } = string.Empty;

    [Range(0, 300, ErrorMessage = "Büyük tansiyon negatif olamaz.")]
    [Display(Name = "Büyük Tansiyon")]
    public int SystolicBloodPressure { get; set; }

    [Range(0, 200, ErrorMessage = "Küçük tansiyon negatif olamaz.")]
    [Display(Name = "Küçük Tansiyon")]
    public int DiastolicBloodPressure { get; set; }

    [Range(0, 250, ErrorMessage = "Nabız negatif olamaz.")]
    [Display(Name = "Nabız")]
    public int Pulse { get; set; }

    [Range(0, 45, ErrorMessage = "Vücut sıcaklığı negatif olamaz.")]
    [Display(Name = "Vücut Sıcaklığı")]
    public decimal BodyTemperature { get; set; }

    [Range(0, 1000, ErrorMessage = "Kan şekeri negatif olamaz.")]
    [Display(Name = "Kan Şekeri")]
    public int BloodSugar { get; set; }

    [Range(0, 1000, ErrorMessage = "Kolesterol negatif olamaz.")]
    [Display(Name = "Kolesterol")]
    public int Cholesterol { get; set; }

    [StringLength(1000)]
    [Display(Name = "Notlar")]
    public string? Notes { get; set; }

    [Required(ErrorMessage = "Kayıt tarihi zorunludur.")]
    [DataType(DataType.Date)]
    [Display(Name = "Kayıt Tarihi")]
    public DateTime RecordDate { get; set; } = DateTime.Today;
}

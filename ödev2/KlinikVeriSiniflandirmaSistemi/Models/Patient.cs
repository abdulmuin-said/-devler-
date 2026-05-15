using System.ComponentModel.DataAnnotations;

namespace KlinikVeriSiniflandirmaSistemi.Models;

public class Patient
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad zorunludur.")]
    [StringLength(100)]
    [Display(Name = "Ad")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad zorunludur.")]
    [StringLength(100)]
    [Display(Name = "Soyad")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasta No veya TC Kimlik No zorunludur.")]
    [StringLength(20)]
    [Display(Name = "Hasta No / TC Kimlik No")]
    public string PatientNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
    [DataType(DataType.Date)]
    [Display(Name = "Doğum Tarihi")]
    public DateTime BirthDate { get; set; }

    [StringLength(20)]
    [Display(Name = "Cinsiyet")]
    public string? Gender { get; set; }

    [StringLength(20)]
    [Phone]
    [Display(Name = "Telefon")]
    public string? Phone { get; set; }

    [StringLength(300)]
    [Display(Name = "Adres")]
    public string? Address { get; set; }

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<ClinicalRecord> ClinicalRecords { get; set; } = new List<ClinicalRecord>();
}

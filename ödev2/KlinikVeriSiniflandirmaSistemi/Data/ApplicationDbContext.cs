using KlinikVeriSiniflandirmaSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace KlinikVeriSiniflandirmaSistemi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();

    public DbSet<ClinicalRecord> ClinicalRecords => Set<ClinicalRecord>();

    public DbSet<ClinicalClass> ClinicalClasses => Set<ClinicalClass>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patients");
            entity.HasIndex(p => p.PatientNumber).IsUnique();
            entity.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.PatientNumber).HasMaxLength(20).IsRequired();
            entity.Property(p => p.BirthDate).HasColumnType("date");
            entity.Property(p => p.CreatedAt).HasColumnType("timestamp without time zone");
            entity.Property(p => p.Gender).HasMaxLength(20);
            entity.Property(p => p.Phone).HasMaxLength(20);
            entity.Property(p => p.Address).HasMaxLength(300);
        });

        modelBuilder.Entity<ClinicalClass>(entity =>
        {
            entity.ToTable("ClinicalClasses");
            entity.HasIndex(c => c.Name).IsUnique();
            entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Description).HasMaxLength(300);
            entity.Property(c => c.RiskLevel).HasMaxLength(100).IsRequired();
            entity.Property(c => c.ColorCode).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<ClinicalRecord>(entity =>
        {
            entity.ToTable("ClinicalRecords");
            entity.Property(r => r.Complaint).HasMaxLength(500).IsRequired();
            entity.Property(r => r.Diagnosis).HasMaxLength(300).IsRequired();
            entity.Property(r => r.BodyTemperature).HasPrecision(4, 1);
            entity.Property(r => r.Notes).HasMaxLength(1000);
            entity.Property(r => r.RecordDate).HasColumnType("date");

            entity.HasOne(r => r.Patient)
                .WithMany(p => p.ClinicalRecords)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.ClinicalClass)
                .WithMany(c => c.ClinicalRecords)
                .HasForeignKey(r => r.ClinicalClassId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ClinicalClass>().HasData(
            new ClinicalClass
            {
                Id = 1,
                Name = "Normal",
                Description = "Değerler normal aralıktadır.",
                RiskLevel = "Düşük Risk",
                ColorCode = "success"
            },
            new ClinicalClass
            {
                Id = 2,
                Name = "Riskli",
                Description = "Bir veya daha fazla klinik değer risk sınırındadır.",
                RiskLevel = "Orta Risk",
                ColorCode = "warning"
            },
            new ClinicalClass
            {
                Id = 3,
                Name = "Acil",
                Description = "Acil değerlendirme gerektiren klinik bulgular vardır.",
                RiskLevel = "Yüksek Risk",
                ColorCode = "danger"
            },
            new ClinicalClass
            {
                Id = 4,
                Name = "Kronik Takip Gerekli",
                Description = "Kronik hastalık veya düzenli takip gereksinimi vardır.",
                RiskLevel = "Orta-Yüksek Risk",
                ColorCode = "orange"
            });

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1,
                FirstName = "Ayşe",
                LastName = "Yılmaz",
                PatientNumber = "10000000001",
                BirthDate = new DateTime(1990, 4, 12),
                Gender = "Kadın",
                Phone = "05551234567",
                Address = "Ankara",
                CreatedAt = new DateTime(2026, 5, 1, 9, 0, 0)
            },
            new Patient
            {
                Id = 2,
                FirstName = "Mehmet",
                LastName = "Demir",
                PatientNumber = "10000000002",
                BirthDate = new DateTime(1978, 9, 3),
                Gender = "Erkek",
                Phone = "05557654321",
                Address = "İstanbul",
                CreatedAt = new DateTime(2026, 5, 4, 10, 30, 0)
            },
            new Patient
            {
                Id = 3,
                FirstName = "Elif",
                LastName = "Kaya",
                PatientNumber = "10000000003",
                BirthDate = new DateTime(1965, 1, 22),
                Gender = "Kadın",
                Phone = "05559876543",
                Address = "İzmir",
                CreatedAt = new DateTime(2026, 5, 8, 14, 15, 0)
            });

        modelBuilder.Entity<ClinicalRecord>().HasData(
            new ClinicalRecord
            {
                Id = 1,
                PatientId = 1,
                ClinicalClassId = 1,
                Complaint = "Rutin kontrol",
                Diagnosis = "Genel durum iyi",
                SystolicBloodPressure = 118,
                DiastolicBloodPressure = 76,
                Pulse = 72,
                BodyTemperature = 36.6m,
                BloodSugar = 95,
                Cholesterol = 180,
                Notes = "Kontrol önerildi.",
                RecordDate = new DateTime(2026, 5, 10)
            },
            new ClinicalRecord
            {
                Id = 2,
                PatientId = 2,
                ClinicalClassId = 2,
                Complaint = "Baş ağrısı ve halsizlik",
                Diagnosis = "Hipertansiyon şüpheli",
                SystolicBloodPressure = 152,
                DiastolicBloodPressure = 96,
                Pulse = 88,
                BodyTemperature = 36.9m,
                BloodSugar = 160,
                Cholesterol = 230,
                Notes = "Tansiyon takibi önerildi.",
                RecordDate = new DateTime(2026, 5, 12)
            },
            new ClinicalRecord
            {
                Id = 3,
                PatientId = 3,
                ClinicalClassId = 4,
                Complaint = "Şeker takibi",
                Diagnosis = "Kronik diyabet",
                SystolicBloodPressure = 135,
                DiastolicBloodPressure = 85,
                Pulse = 82,
                BodyTemperature = 36.8m,
                BloodSugar = 190,
                Cholesterol = 245,
                Notes = "Kronik takip gerekli.",
                RecordDate = new DateTime(2026, 5, 14)
            });
    }
}

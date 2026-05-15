using KlinikVeriSiniflandirmaSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace KlinikVeriSiniflandirmaSistemi.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (await context.ClinicalClasses.AnyAsync())
        {
            return;
        }

        var normal = new ClinicalClass
        {
            Name = "Normal",
            Description = "Değerler normal aralıktadır.",
            RiskLevel = "Düşük Risk",
            ColorCode = "success"
        };

        var risky = new ClinicalClass
        {
            Name = "Riskli",
            Description = "Bir veya daha fazla klinik değer risk sınırındadır.",
            RiskLevel = "Orta Risk",
            ColorCode = "warning"
        };

        var emergency = new ClinicalClass
        {
            Name = "Acil",
            Description = "Acil değerlendirme gerektiren klinik bulgular vardır.",
            RiskLevel = "Yüksek Risk",
            ColorCode = "danger"
        };

        var chronic = new ClinicalClass
        {
            Name = "Kronik Takip Gerekli",
            Description = "Kronik hastalık veya düzenli takip gereksinimi vardır.",
            RiskLevel = "Orta-Yüksek Risk",
            ColorCode = "orange"
        };

        context.ClinicalClasses.AddRange(normal, risky, emergency, chronic);

        var patients = new List<Patient>
        {
            new()
            {
                FirstName = "Ayşe",
                LastName = "Yılmaz",
                PatientNumber = "10000000001",
                BirthDate = new DateTime(1990, 4, 12),
                Gender = "Kadın",
                Phone = "05551234567",
                Address = "Ankara",
                CreatedAt = DateTime.Now.AddDays(-10)
            },
            new()
            {
                FirstName = "Mehmet",
                LastName = "Demir",
                PatientNumber = "10000000002",
                BirthDate = new DateTime(1978, 9, 3),
                Gender = "Erkek",
                Phone = "05557654321",
                Address = "İstanbul",
                CreatedAt = DateTime.Now.AddDays(-6)
            },
            new()
            {
                FirstName = "Elif",
                LastName = "Kaya",
                PatientNumber = "10000000003",
                BirthDate = new DateTime(1965, 1, 22),
                Gender = "Kadın",
                Phone = "05559876543",
                Address = "İzmir",
                CreatedAt = DateTime.Now.AddDays(-2)
            }
        };

        context.Patients.AddRange(patients);
        await context.SaveChangesAsync();

        context.ClinicalRecords.AddRange(
            new ClinicalRecord
            {
                PatientId = patients[0].Id,
                ClinicalClassId = normal.Id,
                Complaint = "Rutin kontrol",
                Diagnosis = "Genel durum iyi",
                SystolicBloodPressure = 118,
                DiastolicBloodPressure = 76,
                Pulse = 72,
                BodyTemperature = 36.6m,
                BloodSugar = 95,
                Cholesterol = 180,
                Notes = "Kontrol önerildi.",
                RecordDate = DateTime.Today.AddDays(-3)
            },
            new ClinicalRecord
            {
                PatientId = patients[1].Id,
                ClinicalClassId = risky.Id,
                Complaint = "Baş ağrısı ve halsizlik",
                Diagnosis = "Hipertansiyon şüpheli",
                SystolicBloodPressure = 152,
                DiastolicBloodPressure = 96,
                Pulse = 88,
                BodyTemperature = 36.9m,
                BloodSugar = 160,
                Cholesterol = 230,
                Notes = "Tansiyon takibi önerildi.",
                RecordDate = DateTime.Today.AddDays(-1)
            },
            new ClinicalRecord
            {
                PatientId = patients[2].Id,
                ClinicalClassId = chronic.Id,
                Complaint = "Şeker takibi",
                Diagnosis = "Kronik diyabet",
                SystolicBloodPressure = 135,
                DiastolicBloodPressure = 85,
                Pulse = 82,
                BodyTemperature = 36.8m,
                BloodSugar = 190,
                Cholesterol = 245,
                Notes = "Kronik takip gerekli.",
                RecordDate = DateTime.Today
            }
        );

        await context.SaveChangesAsync();
    }
}

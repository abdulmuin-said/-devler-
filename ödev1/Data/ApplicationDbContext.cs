using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.Models.Enums;
using KeyloggerTespitSistemi.Services;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<ProcessReport> ProcessReports => Set<ProcessReport>();
    public DbSet<DetectionRule> DetectionRules => Set<DetectionRule>();
    public DbSet<AnalysisResult> AnalysisResults => Set<AnalysisResult>();
    public DbSet<TriggeredRule> TriggeredRules => Set<TriggeredRule>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<ProcessReport>()
            .HasOne(report => report.AnalysisResult)
            .WithOne(result => result.ProcessReport)
            .HasForeignKey<AnalysisResult>(result => result.ProcessReportId);

        modelBuilder.Entity<DetectionRule>()
            .Property(rule => rule.RuleType)
            .HasConversion<string>();

        modelBuilder.Entity<AnalysisResult>()
            .Property(result => result.RiskLevel)
            .HasConversion<string>();

        modelBuilder.Entity<Recommendation>()
            .Property(recommendation => recommendation.RiskLevel)
            .HasConversion<string>();

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Sistem yöneticisi" },
            new Role { Id = 2, Name = "Student", Description = "Analiz yapan öğrenci kullanıcı" });

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FullName = "Admin Kullanıcı",
                Email = "admin@local.test",
                PasswordHash = PasswordHasher.Hash("Admin123!"),
                RoleId = 1,
                IsActive = true,
                CreatedAt = seedDate
            });

        modelBuilder.Entity<DetectionRule>().HasData(
            new DetectionRule { Id = 1, Name = "Bilinmeyen Yayıncı", Description = "Yayıncı bilgisi boş veya bilinmeyen olarak girilmiş processleri işaretler.", RuleType = DetectionRuleType.UnknownPublisher, RiskPoint = 15, IsActive = true },
            new DetectionRule { Id = 2, Name = "Başlangıçta Çalışma", Description = "Sistem açılışında otomatik çalıştığı bildirilen processleri işaretler.", RuleType = DetectionRuleType.RunsAtStartup, RiskPoint = 15, IsActive = true },
            new DetectionRule { Id = 3, Name = "Temp/AppData Konumu", Description = "Temp veya AppData klasöründen çalıştığı bildirilen processleri işaretler.", RuleType = DetectionRuleType.RunsFromTempOrAppData, RiskPoint = 20, IsActive = true },
            new DetectionRule { Id = 4, Name = "Klavye Erişimi Şüphesi", Description = "Klavye erişimi şüphesi manuel olarak bildirilen kayıtları işaretler.", RuleType = DetectionRuleType.KeyboardAccessSuspicion, RiskPoint = 25, IsActive = true },
            new DetectionRule { Id = 5, Name = "Şüpheli Ağ Bağlantısı", Description = "Ağ bağlantısı bulunduğu bildirilen processleri ek risk olarak değerlendirir.", RuleType = DetectionRuleType.SuspiciousNetworkConnection, RiskPoint = 15, IsActive = true },
            new DetectionRule { Id = 6, Name = "Sahte Sistem Dosyası Adı", Description = "Sistem dosyası gibi göründüğü manuel bildirilen adları işaretler.", RuleType = DetectionRuleType.FakeSystemFileName, RiskPoint = 20, IsActive = true },
            new DetectionRule { Id = 7, Name = "Arka Planda Çalışma", Description = "Arka planda çalıştığı bildirilen processleri düşük ek riskle değerlendirir.", RuleType = DetectionRuleType.RunsInBackground, RiskPoint = 10, IsActive = true });

        modelBuilder.Entity<Recommendation>().HasData(
            new Recommendation { Id = 1, RiskLevel = RiskLevel.Low, Title = "Düşük risk takibi", Description = "Process bilgilerini kayıt altında tutun ve yayıncı/dosya yolu değişirse yeniden analiz edin.", IsActive = true },
            new Recommendation { Id = 2, RiskLevel = RiskLevel.Medium, Title = "Manuel inceleme önerilir", Description = "Dosya konumu, yayıncı bilgisi ve başlangıç ayarlarını kontrol edin. Gerekirse güvenlik ekibi veya öğretmenle paylaşın.", IsActive = true },
            new Recommendation { Id = 3, RiskLevel = RiskLevel.High, Title = "İzolasyon ve ayrıntılı analiz", Description = "Bu kayıt yüksek riskli görünmektedir. Canlı sistemde çalıştırmadan izole laboratuvar ortamında savunma amaçlı inceleyin.", IsActive = true });

        modelBuilder.Entity<ProcessReport>().HasData(
            new ProcessReport
            {
                Id = 1,
                UserId = 1,
                ProcessName = "svhost.exe",
                FilePath = "C:\\Users\\Student\\AppData\\Roaming\\svhost.exe",
                Publisher = "Unknown",
                RunsAtStartup = true,
                RunsInBackground = true,
                KeyboardAccessSuspicion = true,
                HasNetworkConnection = true,
                RunsFromTempOrAppData = true,
                FakeSystemFileName = true,
                Description = "Savunma amaçlı örnek şüpheli kayıt.",
                CreatedAt = seedDate
            });

        modelBuilder.Entity<AnalysisResult>().HasData(
            new AnalysisResult
            {
                Id = 1,
                ProcessReportId = 1,
                TotalScore = 100,
                RiskLevel = RiskLevel.High,
                Summary = "Seed kayıt yüksek riskli örnek olarak eklenmiştir.",
                CreatedAt = seedDate
            });

        modelBuilder.Entity<TriggeredRule>().HasData(
            new TriggeredRule { Id = 1, AnalysisResultId = 1, DetectionRuleId = 1, AddedScore = 15, Reason = "Yayıncı bilgisi bilinmeyen olarak değerlendirildi." },
            new TriggeredRule { Id = 2, AnalysisResultId = 1, DetectionRuleId = 2, AddedScore = 15, Reason = "Başlangıçta otomatik çalıştığı bildirildi." },
            new TriggeredRule { Id = 3, AnalysisResultId = 1, DetectionRuleId = 3, AddedScore = 20, Reason = "Temp veya AppData konumundan çalıştığı bildirildi." },
            new TriggeredRule { Id = 4, AnalysisResultId = 1, DetectionRuleId = 4, AddedScore = 25, Reason = "Klavye erişimi şüphesi manuel olarak işaretlendi." },
            new TriggeredRule { Id = 5, AnalysisResultId = 1, DetectionRuleId = 5, AddedScore = 15, Reason = "Ağ bağlantısı bulunduğu bildirildi." },
            new TriggeredRule { Id = 6, AnalysisResultId = 1, DetectionRuleId = 6, AddedScore = 20, Reason = "Sahte sistem dosyası adı şüphesi işaretlendi." },
            new TriggeredRule { Id = 7, AnalysisResultId = 1, DetectionRuleId = 7, AddedScore = 10, Reason = "Arka planda çalışma davranışı bildirildi." });
    }
}

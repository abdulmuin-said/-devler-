using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KeyloggerTespitSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetectionRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RuleType = table.Column<string>(type: "text", nullable: false),
                    RiskPoint = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectionRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RiskLevel = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "character varying(700)", maxLength: 700, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProcessName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Publisher = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    RunsAtStartup = table.Column<bool>(type: "boolean", nullable: false),
                    RunsInBackground = table.Column<bool>(type: "boolean", nullable: false),
                    KeyboardAccessSuspicion = table.Column<bool>(type: "boolean", nullable: false),
                    HasNetworkConnection = table.Column<bool>(type: "boolean", nullable: false),
                    RunsFromTempOrAppData = table.Column<bool>(type: "boolean", nullable: false),
                    FakeSystemFileName = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessReportId = table.Column<int>(type: "integer", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    RiskLevel = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisResults_ProcessReports_ProcessReportId",
                        column: x => x.ProcessReportId,
                        principalTable: "ProcessReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriggeredRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnalysisResultId = table.Column<int>(type: "integer", nullable: false),
                    DetectionRuleId = table.Column<int>(type: "integer", nullable: false),
                    AddedScore = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggeredRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggeredRules_AnalysisResults_AnalysisResultId",
                        column: x => x.AnalysisResultId,
                        principalTable: "AnalysisResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TriggeredRules_DetectionRules_DetectionRuleId",
                        column: x => x.DetectionRuleId,
                        principalTable: "DetectionRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DetectionRules",
                columns: new[] { "Id", "Description", "IsActive", "Name", "RiskPoint", "RuleType" },
                values: new object[,]
                {
                    { 1, "Yayıncı bilgisi boş veya bilinmeyen olarak girilmiş processleri işaretler.", true, "Bilinmeyen Yayıncı", 15, "UnknownPublisher" },
                    { 2, "Sistem açılışında otomatik çalıştığı bildirilen processleri işaretler.", true, "Başlangıçta Çalışma", 15, "RunsAtStartup" },
                    { 3, "Temp veya AppData klasöründen çalıştığı bildirilen processleri işaretler.", true, "Temp/AppData Konumu", 20, "RunsFromTempOrAppData" },
                    { 4, "Klavye erişimi şüphesi manuel olarak bildirilen kayıtları işaretler.", true, "Klavye Erişimi Şüphesi", 25, "KeyboardAccessSuspicion" },
                    { 5, "Ağ bağlantısı bulunduğu bildirilen processleri ek risk olarak değerlendirir.", true, "Şüpheli Ağ Bağlantısı", 15, "SuspiciousNetworkConnection" },
                    { 6, "Sistem dosyası gibi göründüğü manuel bildirilen adları işaretler.", true, "Sahte Sistem Dosyası Adı", 20, "FakeSystemFileName" },
                    { 7, "Arka planda çalıştığı bildirilen processleri düşük ek riskle değerlendirir.", true, "Arka Planda Çalışma", 10, "RunsInBackground" }
                });

            migrationBuilder.InsertData(
                table: "Recommendations",
                columns: new[] { "Id", "Description", "IsActive", "RiskLevel", "Title" },
                values: new object[,]
                {
                    { 1, "Process bilgilerini kayıt altında tutun ve yayıncı/dosya yolu değişirse yeniden analiz edin.", true, "Low", "Düşük risk takibi" },
                    { 2, "Dosya konumu, yayıncı bilgisi ve başlangıç ayarlarını kontrol edin. Gerekirse güvenlik ekibi veya öğretmenle paylaşın.", true, "Medium", "Manuel inceleme önerilir" },
                    { 3, "Bu kayıt yüksek riskli görünmektedir. Canlı sistemde çalıştırmadan izole laboratuvar ortamında savunma amaçlı inceleyin.", true, "High", "İzolasyon ve ayrıntılı analiz" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Sistem yöneticisi", "Admin" },
                    { 2, "Analiz yapan öğrenci kullanıcı", "Student" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "RoleId" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@local.test", "Admin Kullanıcı", true, "86FFC443B908A9836418317AF641C17317C03AA9A9F0FC42C614E70DCE5EFFA8", 1 });

            migrationBuilder.InsertData(
                table: "ProcessReports",
                columns: new[] { "Id", "CreatedAt", "Description", "FakeSystemFileName", "FilePath", "HasNetworkConnection", "KeyboardAccessSuspicion", "ProcessName", "Publisher", "RunsAtStartup", "RunsFromTempOrAppData", "RunsInBackground", "UserId" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma amaçlı örnek şüpheli kayıt.", true, "C:\\Users\\Student\\AppData\\Roaming\\svhost.exe", true, true, "svhost.exe", "Unknown", true, true, true, 1 });

            migrationBuilder.InsertData(
                table: "AnalysisResults",
                columns: new[] { "Id", "CreatedAt", "ProcessReportId", "RiskLevel", "Summary", "TotalScore" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "High", "Seed kayıt yüksek riskli örnek olarak eklenmiştir.", 100 });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisResults_ProcessReportId",
                table: "AnalysisResults",
                column: "ProcessReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessReports_UserId",
                table: "ProcessReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggeredRules_AnalysisResultId",
                table: "TriggeredRules",
                column: "AnalysisResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggeredRules_DetectionRuleId",
                table: "TriggeredRules",
                column: "DetectionRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "TriggeredRules");

            migrationBuilder.DropTable(
                name: "AnalysisResults");

            migrationBuilder.DropTable(
                name: "DetectionRules");

            migrationBuilder.DropTable(
                name: "ProcessReports");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

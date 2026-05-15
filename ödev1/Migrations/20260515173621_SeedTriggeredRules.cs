using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KeyloggerTespitSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedTriggeredRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TriggeredRules",
                columns: new[] { "Id", "AddedScore", "AnalysisResultId", "DetectionRuleId", "Reason" },
                values: new object[,]
                {
                    { 1, 15, 1, 1, "Yayıncı bilgisi bilinmeyen olarak değerlendirildi." },
                    { 2, 15, 1, 2, "Başlangıçta otomatik çalıştığı bildirildi." },
                    { 3, 20, 1, 3, "Temp veya AppData konumundan çalıştığı bildirildi." },
                    { 4, 25, 1, 4, "Klavye erişimi şüphesi manuel olarak işaretlendi." },
                    { 5, 15, 1, 5, "Ağ bağlantısı bulunduğu bildirildi." },
                    { 6, 20, 1, 6, "Sahte sistem dosyası adı şüphesi işaretlendi." },
                    { 7, 10, 1, 7, "Arka planda çalışma davranışı bildirildi." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TriggeredRules",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}

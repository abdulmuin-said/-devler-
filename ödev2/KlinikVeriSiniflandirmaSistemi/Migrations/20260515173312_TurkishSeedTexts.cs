using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlinikVeriSiniflandirmaSistemi.Migrations
{
    /// <inheritdoc />
    public partial class TurkishSeedTexts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "RiskLevel" },
                values: new object[] { "Değerler normal aralıktadır.", "Düşük Risk" });

            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Bir veya daha fazla klinik değer risk sınırındadır.");

            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "RiskLevel" },
                values: new object[] { "Acil değerlendirme gerektiren klinik bulgular vardır.", "Yüksek Risk" });

            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "RiskLevel" },
                values: new object[] { "Kronik hastalık veya düzenli takip gereksinimi vardır.", "Orta-Yüksek Risk" });

            migrationBuilder.UpdateData(
                table: "ClinicalRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "Notes",
                value: "Kontrol önerildi.");

            migrationBuilder.UpdateData(
                table: "ClinicalRecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Complaint", "Diagnosis", "Notes" },
                values: new object[] { "Baş ağrısı ve halsizlik", "Hipertansiyon şüpheli", "Tansiyon takibi önerildi." });

            migrationBuilder.UpdateData(
                table: "ClinicalRecords",
                keyColumn: "Id",
                keyValue: 3,
                column: "Complaint",
                value: "Şeker takibi");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "Gender", "LastName" },
                values: new object[] { "Ayşe", "Kadın", "Yılmaz" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "İstanbul");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Gender" },
                values: new object[] { "İzmir", "Kadın" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "RiskLevel" },
                values: new object[] { "Değerler normal aralıktadır.", "Düşük Risk" });

            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Bir veya daha fazla klinik değer risk sınırındadır.");

            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "RiskLevel" },
                values: new object[] { "Acil değerlendirme gerektiren klinik bulgular vardır.", "Yüksek Risk" });

            migrationBuilder.UpdateData(
                table: "ClinicalClasses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "RiskLevel" },
                values: new object[] { "Kronik hastalık veya düzenli takip gereksinimi vardır.", "Orta-Yüksek Risk" });

            migrationBuilder.UpdateData(
                table: "ClinicalRecords",
                keyColumn: "Id",
                keyValue: 1,
                column: "Notes",
                value: "Kontrol önerildi.");

            migrationBuilder.UpdateData(
                table: "ClinicalRecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Complaint", "Diagnosis", "Notes" },
                values: new object[] { "Baş ağrısı ve halsizlik", "Hipertansiyon şüpheli", "Tansiyon takibi önerildi." });

            migrationBuilder.UpdateData(
                table: "ClinicalRecords",
                keyColumn: "Id",
                keyValue: 3,
                column: "Complaint",
                value: "Şeker takibi");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "Gender", "LastName" },
                values: new object[] { "Ayşe", "Kadın", "Yılmaz" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "İstanbul");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Gender" },
                values: new object[] { "İzmir", "Kadın" });
        }
    }
}

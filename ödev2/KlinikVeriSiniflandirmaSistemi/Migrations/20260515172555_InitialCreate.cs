using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KlinikVeriSiniflandirmaSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicalClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    RiskLevel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ColorCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PatientNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    ClinicalClassId = table.Column<int>(type: "integer", nullable: false),
                    Complaint = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Diagnosis = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    SystolicBloodPressure = table.Column<int>(type: "integer", nullable: false),
                    DiastolicBloodPressure = table.Column<int>(type: "integer", nullable: false),
                    Pulse = table.Column<int>(type: "integer", nullable: false),
                    BodyTemperature = table.Column<decimal>(type: "numeric(4,1)", precision: 4, scale: 1, nullable: false),
                    BloodSugar = table.Column<int>(type: "integer", nullable: false),
                    Cholesterol = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    RecordDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalRecords_ClinicalClasses_ClinicalClassId",
                        column: x => x.ClinicalClassId,
                        principalTable: "ClinicalClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClinicalClasses",
                columns: new[] { "Id", "ColorCode", "Description", "Name", "RiskLevel" },
                values: new object[,]
                {
                    { 1, "success", "Değerler normal aralıktadır.", "Normal", "Düşük Risk" },
                    { 2, "warning", "Bir veya daha fazla klinik değer risk sınırındadır.", "Riskli", "Orta Risk" },
                    { 3, "danger", "Acil değerlendirme gerektiren klinik bulgular vardır.", "Acil", "Yüksek Risk" },
                    { 4, "orange", "Kronik hastalık veya düzenli takip gereksinimi vardır.", "Kronik Takip Gerekli", "Orta-Yüksek Risk" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "BirthDate", "CreatedAt", "FirstName", "Gender", "LastName", "PatientNumber", "Phone" },
                values: new object[,]
                {
                    { 1, "Ankara", new DateTime(1990, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Ayşe", "Kadın", "Yılmaz", "10000000001", "05551234567" },
                    { 2, "İstanbul", new DateTime(1978, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 4, 10, 30, 0, 0, DateTimeKind.Unspecified), "Mehmet", "Erkek", "Demir", "10000000002", "05557654321" },
                    { 3, "İzmir", new DateTime(1965, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 8, 14, 15, 0, 0, DateTimeKind.Unspecified), "Elif", "Kadın", "Kaya", "10000000003", "05559876543" }
                });

            migrationBuilder.InsertData(
                table: "ClinicalRecords",
                columns: new[] { "Id", "BloodSugar", "BodyTemperature", "Cholesterol", "ClinicalClassId", "Complaint", "Diagnosis", "DiastolicBloodPressure", "Notes", "PatientId", "Pulse", "RecordDate", "SystolicBloodPressure" },
                values: new object[,]
                {
                    { 1, 95, 36.6m, 180, 1, "Rutin kontrol", "Genel durum iyi", 76, "Kontrol önerildi.", 1, 72, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 118 },
                    { 2, 160, 36.9m, 230, 2, "Baş ağrısı ve halsizlik", "Hipertansiyon şüpheli", 96, "Tansiyon takibi önerildi.", 2, 88, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 152 },
                    { 3, 190, 36.8m, 245, 4, "Şeker takibi", "Kronik diyabet", 85, "Kronik takip gerekli.", 3, 82, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 135 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalClasses_Name",
                table: "ClinicalClasses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecords_ClinicalClassId",
                table: "ClinicalRecords",
                column: "ClinicalClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecords_PatientId",
                table: "ClinicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientNumber",
                table: "Patients",
                column: "PatientNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalRecords");

            migrationBuilder.DropTable(
                name: "ClinicalClasses");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}

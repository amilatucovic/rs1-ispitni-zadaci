using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_25.API.Migrations
{
    /// <inheritdoc />
    public partial class semesters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WinterSemesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    RecordedById = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YearOfStudy = table.Column<int>(type: "int", nullable: false),
                    Tuition = table.Column<float>(type: "real", nullable: false),
                    Renewal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinterSemesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WinterSemesters_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WinterSemesters_MyAppUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WinterSemesters_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WinterSemesters_AcademicYearId",
                table: "WinterSemesters",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_WinterSemesters_RecordedById",
                table: "WinterSemesters",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_WinterSemesters_StudentId",
                table: "WinterSemesters",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WinterSemesters");
        }
    }
}

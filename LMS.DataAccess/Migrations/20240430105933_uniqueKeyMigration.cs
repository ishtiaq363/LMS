using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class uniqueKeyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Student_RegistrationNo",
                table: "Student",
                column: "RegistrationNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchStudent_EnrollmentNo",
                table: "BatchStudent",
                column: "EnrollmentNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_RegistrationNo",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_BatchStudent_EnrollmentNo",
                table: "BatchStudent");
        }
    }
}

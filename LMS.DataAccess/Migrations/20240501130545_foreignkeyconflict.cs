using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyconflict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentDetail_StudentId",
                table: "StudentDetail");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetail_StudentId",
                table: "StudentDetail",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentDetail_StudentId",
                table: "StudentDetail");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetail_StudentId",
                table: "StudentDetail",
                column: "StudentId");
        }
    }
}

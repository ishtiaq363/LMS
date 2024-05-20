using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class migrationinit112233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentSchedule_Course_CourseId",
                table: "AssessmentSchedule");

            migrationBuilder.DropIndex(
                name: "IX_Submission_AssessmentScheduleId",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentSchedule_CourseId",
                table: "AssessmentSchedule");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "AssessmentSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_AssessmentScheduleId",
                table: "Submission",
                column: "AssessmentScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submission_AssessmentScheduleId",
                table: "Submission");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "AssessmentSchedule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submission_AssessmentScheduleId",
                table: "Submission",
                column: "AssessmentScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSchedule_CourseId",
                table: "AssessmentSchedule",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentSchedule_Course_CourseId",
                table: "AssessmentSchedule",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}

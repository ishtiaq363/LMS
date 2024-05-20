using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class notification11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Submission",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubjectDetail",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subject",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentDetail",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Student",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Payment",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseSubject",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Course",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BatchStudent",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BatchFee",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Batch",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AssessmentSchedule",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Assessment",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubjectOutline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Importance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objectives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundamentalPrinciples = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoreConcepts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealWorldApplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Implications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyResearchers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorStudies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResearchMethodologies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Textbooks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlineResources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalReferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradingRubrics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectOutline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectOutline_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectOutline_SubjectId",
                table: "SubjectOutline",
                column: "SubjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectOutline");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubjectDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseSubject");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BatchStudent");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BatchFee");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AssessmentSchedule");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Assessment");
        }
    }
}

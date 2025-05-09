using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class createExamtable : Migration
{
    private static readonly string[] columns = new[] { "start_time", "end_time" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_EXAM",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                title = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                total_marks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                passing_marks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                instructor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                instructions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_exam", x => x.id);
                table.CheckConstraint("CK_Exams_Status", "[Status] IN ('Scheduled', 'InProgress', 'Completed', 'PostPoned', 'Cancelled')");
                table.ForeignKey(
                    name: "fk_tbl_exam_tbl_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_COURSES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_exam_tbl_users_instructor_id",
                    column: x => x.instructor_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_course_id",
            schema: "dbo",
            table: "TBL_EXAM",
            column: "course_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_instructor_id",
            schema: "dbo",
            table: "TBL_EXAM",
            column: "instructor_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_start_time_end_time",
            schema: "dbo",
            table: "TBL_EXAM",
            columns: columns);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_EXAM",
            schema: "dbo");
    }
}

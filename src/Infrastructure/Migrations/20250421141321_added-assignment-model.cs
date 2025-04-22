using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class Addedassignmentmodel : Migration
{
    private static readonly string[] columns = new[] { "id", "name" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_ASSIGNMENT_TYPES",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_tbl_assignment_types", x => x.id));

        migrationBuilder.CreateTable(
            name: "TBL_ASSIGNMENT",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                collection_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                max_score = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                assignment_type_id = table.Column<int>(type: "int", nullable: true),
                due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_assignment", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_assignment_assignment_types_assignment_type_id",
                    column: x => x.assignment_type_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_ASSIGNMENT_TYPES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_assignment_tbl_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_COURSES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "TBL_ASSIGNMENT_SUBMISSION",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                submitted_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                submission_text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                file_url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                grade = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: true),
                feedback = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                graded_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_assignment_submission", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_assignment_submission_tbl_assignment_assignment_id",
                    column: x => x.assignment_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_ASSIGNMENT",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_assignment_submission_tbl_users_submitted_by_id",
                    column: x => x.submitted_by_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            schema: "dbo",
            table: "TBL_ASSIGNMENT_TYPES",
            columns: columns,
            values: new object[,]
            {
                { 1, "QUIZ" },
                { 2, "ESSAY" },
                { 3, "UPLOAD" },
                { 4, "MULTIPLE_CHOICE" }
            });

        migrationBuilder.InsertData(
            schema: "dbo",
            table: "TBL_ROLES",
            columns: columns,
            values: new object[] { 4, "Admin" });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_assignment_assignment_type_id",
            schema: "dbo",
            table: "TBL_ASSIGNMENT",
            column: "assignment_type_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_assignment_course_id",
            schema: "dbo",
            table: "TBL_ASSIGNMENT",
            column: "course_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_assignment_submission_assignment_id",
            schema: "dbo",
            table: "TBL_ASSIGNMENT_SUBMISSION",
            column: "assignment_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_assignment_submission_submitted_by_id",
            schema: "dbo",
            table: "TBL_ASSIGNMENT_SUBMISSION",
            column: "submitted_by_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_ASSIGNMENT_SUBMISSION",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_ASSIGNMENT",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_ASSIGNMENT_TYPES",
            schema: "dbo");

        migrationBuilder.DeleteData(
            schema: "dbo",
            table: "TBL_ROLES",
            keyColumn: "id",
            keyValue: 4);
    }
}

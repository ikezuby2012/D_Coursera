using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class CourseTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_COURSES",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                duration = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                availability = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                instructor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_courses", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_courses_tbl_users_instructor_id",
                    column: x => x.instructor_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_courses_instructor_id",
            schema: "dbo",
            table: "TBL_COURSES",
            column: "instructor_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_COURSES",
            schema: "dbo");
    }
}

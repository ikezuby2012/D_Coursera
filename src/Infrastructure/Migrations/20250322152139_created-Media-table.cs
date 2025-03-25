using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class createdMediatable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_MEDIA",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                collection_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                media_url = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_media", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_media_tbl_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_COURSES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_media_course_id",
            schema: "dbo",
            table: "TBL_MEDIA",
            column: "course_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_MEDIA",
            schema: "dbo");
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class createCourseTimeline : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_CourseTimelineMedia",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                media_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                file_format = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                file_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                uploaded_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_course_timeline_media", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_course_timeline_media_tbl_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_COURSES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_tbl_course_timeline_media_tbl_users_uploaded_by_id",
                    column: x => x.uploaded_by_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_course_timeline_media_course_id",
            schema: "dbo",
            table: "TBL_CourseTimelineMedia",
            column: "course_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_course_timeline_media_uploaded_by_id",
            schema: "dbo",
            table: "TBL_CourseTimelineMedia",
            column: "uploaded_by_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_CourseTimelineMedia",
            schema: "dbo");
    }
}

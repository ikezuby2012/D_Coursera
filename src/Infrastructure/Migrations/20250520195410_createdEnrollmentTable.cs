using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class createdEnrollmentTable : Migration
{
    private static readonly string[] columns = new[] { "id", "name" };
    private static readonly string[] columnsArray = new[] { "course_id", "user_id" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_ENROLLMENT_STATUS",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_tbl_enrollment_status", x => x.id));

        migrationBuilder.CreateTable(
            name: "TBL_ENROLLMENT",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                status_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_enrollment", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_enrollment_enrollment_status_status_id",
                    column: x => x.status_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_ENROLLMENT_STATUS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_enrollment_tbl_courses_course_id",
                    column: x => x.course_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_COURSES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_enrollment_tbl_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            schema: "dbo",
            table: "TBL_ENROLLMENT_STATUS",
            columns: columns,
            values: new object[,]
            {
                { 1, "Pending" },
                { 2, "Approved" },
                { 3, "Rejected" },
                { 4, "Completed" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_enrollment_course_id_user_id",
            schema: "dbo",
            table: "TBL_ENROLLMENT",
            columns: columnsArray,
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_tbl_enrollment_status_id",
            schema: "dbo",
            table: "TBL_ENROLLMENT",
            column: "status_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_enrollment_user_id",
            schema: "dbo",
            table: "TBL_ENROLLMENT",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_ENROLLMENT",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_ENROLLMENT_STATUS",
            schema: "dbo");
    }
}

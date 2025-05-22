using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class updatedCourseTableWithCapacity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "capacity",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "int",
            nullable: true,
            defaultValue: 500);

        migrationBuilder.AddColumn<bool>(
            name: "is_paid",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "prerequisites",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "nvarchar(4000)",
            maxLength: 4000,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "capacity",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "is_paid",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "prerequisites",
            schema: "dbo",
            table: "TBL_COURSES");
    }
}

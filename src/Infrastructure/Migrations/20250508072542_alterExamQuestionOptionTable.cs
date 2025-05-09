using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class alterExamQuestionOptionTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "option_label",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION",
            type: "nvarchar(3000)",
            maxLength: 3000,
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "option_label",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION");
    }
}

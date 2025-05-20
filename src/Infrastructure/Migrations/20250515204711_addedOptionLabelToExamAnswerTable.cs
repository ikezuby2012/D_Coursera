using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class addedOptionLabelToExamAnswerTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "option_label",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER",
            type: "nvarchar(4000)",
            maxLength: 4000,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "option_label",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER");
    }
}

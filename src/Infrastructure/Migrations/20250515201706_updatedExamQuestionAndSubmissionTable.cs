using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class updatedExamQuestionAndSubmissionTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "graded_at",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "graded_by_id",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER",
            type: "nvarchar(150)",
            maxLength: 150,
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "is_graded",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "graded_at",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER");

        migrationBuilder.DropColumn(
            name: "graded_by_id",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER");

        migrationBuilder.DropColumn(
            name: "is_graded",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER");
    }
}

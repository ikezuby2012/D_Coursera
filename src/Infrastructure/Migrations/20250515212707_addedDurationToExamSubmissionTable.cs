using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class addedDurationToExamSubmissionTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "end_time",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "start_time",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "end_time",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION");

        migrationBuilder.DropColumn(
            name: "start_time",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION");
    }
}

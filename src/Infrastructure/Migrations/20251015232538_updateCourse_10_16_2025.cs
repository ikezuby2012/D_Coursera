using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class updateCourse_10_16_2025 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_tbl_exam_questions_exam_type_type_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS");

        migrationBuilder.DropForeignKey(
            name: "fk_tbl_exam_questions_tbl_exam_exam_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS");

        migrationBuilder.AddColumn<string>(
            name: "category",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "course_level",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateTime>(
            name: "end_date",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "language",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateTime>(
            name: "start_date",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "time_zone",
            schema: "dbo",
            table: "TBL_COURSES",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddForeignKey(
            name: "fk_tbl_exam_questions_exam_type_type_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS",
            column: "type_id",
            principalSchema: "dbo",
            principalTable: "TBL_EXAM_TYPES",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_tbl_exam_questions_tbl_exam_exam_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS",
            column: "exam_id",
            principalSchema: "dbo",
            principalTable: "TBL_EXAM",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_tbl_exam_questions_exam_type_type_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS");

        migrationBuilder.DropForeignKey(
            name: "fk_tbl_exam_questions_tbl_exam_exam_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS");

        migrationBuilder.DropColumn(
            name: "category",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "course_level",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "end_date",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "language",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "start_date",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.DropColumn(
            name: "time_zone",
            schema: "dbo",
            table: "TBL_COURSES");

        migrationBuilder.AddForeignKey(
            name: "fk_tbl_exam_questions_exam_type_type_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS",
            column: "type_id",
            principalSchema: "dbo",
            principalTable: "TBL_EXAM_TYPES",
            principalColumn: "id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "fk_tbl_exam_questions_tbl_exam_exam_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS",
            column: "exam_id",
            principalSchema: "dbo",
            principalTable: "TBL_EXAM",
            principalColumn: "id",
            onDelete: ReferentialAction.Restrict);
    }
}

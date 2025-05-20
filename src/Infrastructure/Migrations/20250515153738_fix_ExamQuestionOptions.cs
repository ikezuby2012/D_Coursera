using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class fix_ExamQuestionOptions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_tbl_exam_question_option_tbl_exam_questions_exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION");

        migrationBuilder.DropIndex(
            name: "ix_tbl_exam_question_option_exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION");

        migrationBuilder.DropColumn(
            name: "exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_question_option_exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION",
            column: "exam_questions_id");

        migrationBuilder.AddForeignKey(
            name: "fk_tbl_exam_question_option_tbl_exam_questions_exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION",
            column: "exam_questions_id",
            principalSchema: "dbo",
            principalTable: "TBL_EXAM_QUESTIONS",
            principalColumn: "id");
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class createExamDependenciesTable : Migration
{
    private static readonly string[] columns = new[] { "id", "name" };
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBL_EXAM_SUBMISSION",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                graded_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                graded_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                total_score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                is_graded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_exam_submission", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_exam_submission_tbl_exam_exam_id",
                    column: x => x.exam_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_exam_submission_tbl_users_graded_by_id",
                    column: x => x.graded_by_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_exam_submission_tbl_users_student_id",
                    column: x => x.student_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "TBL_EXAM_TYPES",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_tbl_exam_types", x => x.id));

        migrationBuilder.CreateTable(
            name: "TBL_EXAM_QUESTIONS",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                exam_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                question_text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                type_id = table.Column<int>(type: "int", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_exam_questions", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_exam_questions_exam_type_type_id",
                    column: x => x.type_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM_TYPES",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_exam_questions_tbl_exam_exam_id",
                    column: x => x.exam_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "TBL_EXAM_ANSWER",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                submission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                answer_text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                is_correct = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_exam_answer", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_exam_answer_tbl_exam_questions_question_id",
                    column: x => x.question_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM_QUESTIONS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_tbl_exam_answer_tbl_exam_submission_submission_id",
                    column: x => x.submission_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM_SUBMISSION",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "TBL_EXAM_QUESTION_OPTION",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                question_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                option_text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                is_correct = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                created_by_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                modified_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                is_soft_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                exam_questions_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_exam_question_option", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_exam_question_option_tbl_exam_questions_exam_questions_id",
                    column: x => x.exam_questions_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM_QUESTIONS",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_tbl_exam_question_option_tbl_exam_questions_question_id",
                    column: x => x.question_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_EXAM_QUESTIONS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            schema: "dbo",
            table: "TBL_EXAM_TYPES",
            columns: columns,
            values: new object[,]
            {
                { 1, "Multiple Choice" },
                { 2, "True or False" },
                { 3, "Fill In The Blank" },
                { 4, "Match The Following" },
                { 5, "Brief Answer Only" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_answer_question_id",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER",
            column: "question_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_answer_submission_id",
            schema: "dbo",
            table: "TBL_EXAM_ANSWER",
            column: "submission_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_question_option_exam_questions_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION",
            column: "exam_questions_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_question_option_question_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTION_OPTION",
            column: "question_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_questions_exam_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS",
            column: "exam_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_questions_type_id",
            schema: "dbo",
            table: "TBL_EXAM_QUESTIONS",
            column: "type_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_submission_exam_id",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION",
            column: "exam_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_submission_graded_by_id",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION",
            column: "graded_by_id");

        migrationBuilder.CreateIndex(
            name: "ix_tbl_exam_submission_student_id",
            schema: "dbo",
            table: "TBL_EXAM_SUBMISSION",
            column: "student_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TBL_EXAM_ANSWER",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_EXAM_QUESTION_OPTION",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_EXAM_SUBMISSION",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_EXAM_QUESTIONS",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_EXAM_TYPES",
            schema: "dbo");
    }
}

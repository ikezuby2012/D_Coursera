using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateUserTable : Migration
{
    private static readonly string[] columns = new[] { "id", "name" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dbo");

        migrationBuilder.CreateTable(
            name: "TBL_ROLES",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_tbl_roles", x => x.id));

        migrationBuilder.CreateTable(
            name: "TBL_USERS",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EMAIL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                FIRST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                LAST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                OTP = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                PASSWORD_HASH = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                ROLE_ID = table.Column<int>(type: "int", nullable: true),
                CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                LAST_LOGIN = table.Column<DateTime>(type: "datetime2", nullable: true),
                CREATED_BY_ID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                IS_SOFT_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                IS_VERIFIED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                MODIFIED_BY = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tbl_users", x => x.id);
                table.ForeignKey(
                    name: "fk_tbl_users_tbl_roles_role_id",
                    column: x => x.ROLE_ID,
                    principalSchema: "dbo",
                    principalTable: "TBL_ROLES",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "todo_items",
            schema: "dbo",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                due_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                labels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                is_completed = table.Column<bool>(type: "bit", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                completed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                priority = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_todo_items", x => x.id);
                table.ForeignKey(
                    name: "fk_todo_items_tbl_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "dbo",
                    principalTable: "TBL_USERS",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "dbo",
            table: "TBL_ROLES",
            columns: columns,
            values: new object[,]
            {
                { 1, "User" },
                { 2, "Business_Developer" },
                { 3, "Instructor" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_tbl_users_email",
            schema: "dbo",
            table: "TBL_USERS",
            column: "EMAIL",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_tbl_users_role_id",
            schema: "dbo",
            table: "TBL_USERS",
            column: "ROLE_ID");

        migrationBuilder.CreateIndex(
            name: "ix_todo_items_user_id",
            schema: "dbo",
            table: "todo_items",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "todo_items",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_USERS",
            schema: "dbo");

        migrationBuilder.DropTable(
            name: "TBL_ROLES",
            schema: "dbo");
    }
}

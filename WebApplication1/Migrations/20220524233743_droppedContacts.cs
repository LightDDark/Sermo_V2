using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class droppedContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_UserId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Log_LogstringId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_Contact_UserId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_AuthorId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Log_UserId",
                table: "Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_LogstringId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "LogstringId",
                table: "Contact");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_UserId",
                table: "User",
                newName: "IX_User_UserId");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Password",
                keyValue: null,
                column: "Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LogUser",
                columns: table => new
                {
                    LogsstringId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsersId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUser", x => new { x.LogsstringId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LogUser_Log_LogsstringId",
                        column: x => x.LogsstringId,
                        principalTable: "Log",
                        principalColumn: "stringId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LogUser_UsersId",
                table: "LogUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_AuthorId",
                table: "Message",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UserId",
                table: "User",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_AuthorId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UserId",
                table: "User");

            migrationBuilder.DropTable(
                name: "LogUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Contact");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserId",
                table: "Contact",
                newName: "IX_Contact_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Log",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Contact",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Contact",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LogstringId",
                table: "Contact",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserId",
                table: "Log",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LogstringId",
                table: "Contact",
                column: "LogstringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Contact_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "Contact",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Log_LogstringId",
                table: "Contact",
                column: "LogstringId",
                principalTable: "Log",
                principalColumn: "stringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Contact_UserId",
                table: "Log",
                column: "UserId",
                principalTable: "Contact",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_AuthorId",
                table: "Message",
                column: "AuthorId",
                principalTable: "Contact",
                principalColumn: "Id");
        }
    }
}

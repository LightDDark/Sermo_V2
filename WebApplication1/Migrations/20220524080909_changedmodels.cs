using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class changedmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogUser_User_UsersName",
                table: "LogUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_AuthorName",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UserName",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Message_AuthorName",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "User",
                newName: "Server");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserName",
                table: "User",
                newName: "IX_User_UserId");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Message",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UsersName",
                table: "LogUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_LogUser_UsersName",
                table: "LogUser",
                newName: "IX_LogUser_UsersId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "User",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Last",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Lastdate",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Message",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_AuthorId",
                table: "Message",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogUser_User_UsersId",
                table: "LogUser",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_LogUser_User_UsersId",
                table: "LogUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_AuthorId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UserId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Message_AuthorId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Last",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Lastdate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Server",
                table: "User",
                newName: "Nickname");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserId",
                table: "User",
                newName: "IX_User_UserName");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Message",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "LogUser",
                newName: "UsersName");

            migrationBuilder.RenameIndex(
                name: "IX_LogUser_UsersId",
                table: "LogUser",
                newName: "IX_LogUser_UsersName");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Message",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Message_AuthorName",
                table: "Message",
                column: "AuthorName");

            migrationBuilder.AddForeignKey(
                name: "FK_LogUser_User_UsersName",
                table: "LogUser",
                column: "UsersName",
                principalTable: "User",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_AuthorName",
                table: "Message",
                column: "AuthorName",
                principalTable: "User",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UserName",
                table: "User",
                column: "UserName",
                principalTable: "User",
                principalColumn: "Name");
        }
    }
}

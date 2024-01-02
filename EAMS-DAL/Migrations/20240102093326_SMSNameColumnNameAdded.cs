using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class SMSNameColumnNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserState_AspNetUsers_Id",
                table: "UserState");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserState",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "SMSName",
                table: "SMSTemplate",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserState_AspNetUsers_Id",
                table: "UserState",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserState_AspNetUsers_Id",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "SMSName",
                table: "SMSTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserState",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserState_AspNetUsers_Id",
                table: "UserState",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

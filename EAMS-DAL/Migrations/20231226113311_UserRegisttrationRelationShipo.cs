using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegisttrationRelationShipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssembly_AspNetUsers_Id",
                table: "UserAssembly");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDistrict_AspNetUsers_Id",
                table: "UserDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPCConstituency_AspNetUsers_Id",
                table: "UserPCConstituency");

            migrationBuilder.DropForeignKey(
                name: "FK_UserState_AspNetUsers_Id",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserState_Id",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserPCConstituency_Id",
                table: "UserPCConstituency");

            migrationBuilder.DropIndex(
                name: "IX_UserDistrict_Id",
                table: "UserDistrict");

            migrationBuilder.DropIndex(
                name: "IX_UserAssembly_Id",
                table: "UserAssembly");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPCConstituency");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserDistrict");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAssembly");

            migrationBuilder.AlterColumn<string>(
                name: "UserRegistrationId",
                table: "UserState",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserState",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserRegistrationId",
                table: "UserPCConstituency",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserPCConstituency",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserRegistrationId",
                table: "UserDistrict",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserDistrict",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserRegistrationId",
                table: "UserAssembly",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserState_UserRegistrationId",
                table: "UserState",
                column: "UserRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPCConstituency_UserRegistrationId",
                table: "UserPCConstituency",
                column: "UserRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDistrict_UserRegistrationId",
                table: "UserDistrict",
                column: "UserRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssembly_UserRegistrationId",
                table: "UserAssembly",
                column: "UserRegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssembly_AspNetUsers_UserRegistrationId",
                table: "UserAssembly",
                column: "UserRegistrationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDistrict_AspNetUsers_UserRegistrationId",
                table: "UserDistrict",
                column: "UserRegistrationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPCConstituency_AspNetUsers_UserRegistrationId",
                table: "UserPCConstituency",
                column: "UserRegistrationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserState_AspNetUsers_UserRegistrationId",
                table: "UserState",
                column: "UserRegistrationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssembly_AspNetUsers_UserRegistrationId",
                table: "UserAssembly");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDistrict_AspNetUsers_UserRegistrationId",
                table: "UserDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPCConstituency_AspNetUsers_UserRegistrationId",
                table: "UserPCConstituency");

            migrationBuilder.DropForeignKey(
                name: "FK_UserState_AspNetUsers_UserRegistrationId",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserState_UserRegistrationId",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserPCConstituency_UserRegistrationId",
                table: "UserPCConstituency");

            migrationBuilder.DropIndex(
                name: "IX_UserDistrict_UserRegistrationId",
                table: "UserDistrict");

            migrationBuilder.DropIndex(
                name: "IX_UserAssembly_UserRegistrationId",
                table: "UserAssembly");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserPCConstituency");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserDistrict");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAssembly");

            migrationBuilder.AlterColumn<int>(
                name: "UserRegistrationId",
                table: "UserState",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserState",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserRegistrationId",
                table: "UserPCConstituency",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserPCConstituency",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserRegistrationId",
                table: "UserDistrict",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserDistrict",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserRegistrationId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserAssembly",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserState_Id",
                table: "UserState",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPCConstituency_Id",
                table: "UserPCConstituency",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDistrict_Id",
                table: "UserDistrict",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssembly_Id",
                table: "UserAssembly",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssembly_AspNetUsers_Id",
                table: "UserAssembly",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDistrict_AspNetUsers_Id",
                table: "UserDistrict",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPCConstituency_AspNetUsers_Id",
                table: "UserPCConstituency",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserState_AspNetUsers_Id",
                table: "UserState",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

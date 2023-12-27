using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegisttrationRelationShip1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAssembly_UserAssemblyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserDistrict_UserDistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserPCConstituency_UserPCConstituencyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserState_UserStateId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserAssemblyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserDistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserPCConstituencyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserStateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAssemblyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserDistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPCConstituencyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserStateId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserState",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserRegistrationId",
                table: "UserState",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserPCConstituency",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserRegistrationId",
                table: "UserPCConstituency",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserDistrict",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserRegistrationId",
                table: "UserDistrict",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserAssembly",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserRegistrationId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "UserRegistrationId",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPCConstituency");

            migrationBuilder.DropColumn(
                name: "UserRegistrationId",
                table: "UserPCConstituency");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserDistrict");

            migrationBuilder.DropColumn(
                name: "UserRegistrationId",
                table: "UserDistrict");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAssembly");

            migrationBuilder.DropColumn(
                name: "UserRegistrationId",
                table: "UserAssembly");

            migrationBuilder.AddColumn<int>(
                name: "UserAssemblyId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserDistrictId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserPCConstituencyId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserStateId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserAssemblyId",
                table: "AspNetUsers",
                column: "UserAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserDistrictId",
                table: "AspNetUsers",
                column: "UserDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserPCConstituencyId",
                table: "AspNetUsers",
                column: "UserPCConstituencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserStateId",
                table: "AspNetUsers",
                column: "UserStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAssembly_UserAssemblyId",
                table: "AspNetUsers",
                column: "UserAssemblyId",
                principalTable: "UserAssembly",
                principalColumn: "UserAssemblyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserDistrict_UserDistrictId",
                table: "AspNetUsers",
                column: "UserDistrictId",
                principalTable: "UserDistrict",
                principalColumn: "UserDistrictId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserPCConstituency_UserPCConstituencyId",
                table: "AspNetUsers",
                column: "UserPCConstituencyId",
                principalTable: "UserPCConstituency",
                principalColumn: "UserPCConstituencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserState_UserStateId",
                table: "AspNetUsers",
                column: "UserStateId",
                principalTable: "UserState",
                principalColumn: "UserStateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

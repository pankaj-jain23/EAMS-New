using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegistrationRelationChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDistrictId",
                table: "UserState",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserPCConstituencyId",
                table: "UserState",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserAssemblyId",
                table: "UserPCConstituency",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserAssemblyId",
                table: "UserDistrict",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserState_UserDistrictId",
                table: "UserState",
                column: "UserDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_UserState_UserPCConstituencyId",
                table: "UserState",
                column: "UserPCConstituencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPCConstituency_UserAssemblyId",
                table: "UserPCConstituency",
                column: "UserAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDistrict_UserAssemblyId",
                table: "UserDistrict",
                column: "UserAssemblyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDistrict_UserAssembly_UserAssemblyId",
                table: "UserDistrict",
                column: "UserAssemblyId",
                principalTable: "UserAssembly",
                principalColumn: "UserAssemblyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPCConstituency_UserAssembly_UserAssemblyId",
                table: "UserPCConstituency",
                column: "UserAssemblyId",
                principalTable: "UserAssembly",
                principalColumn: "UserAssemblyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserState_UserDistrict_UserDistrictId",
                table: "UserState",
                column: "UserDistrictId",
                principalTable: "UserDistrict",
                principalColumn: "UserDistrictId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserState_UserPCConstituency_UserPCConstituencyId",
                table: "UserState",
                column: "UserPCConstituencyId",
                principalTable: "UserPCConstituency",
                principalColumn: "UserPCConstituencyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDistrict_UserAssembly_UserAssemblyId",
                table: "UserDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPCConstituency_UserAssembly_UserAssemblyId",
                table: "UserPCConstituency");

            migrationBuilder.DropForeignKey(
                name: "FK_UserState_UserDistrict_UserDistrictId",
                table: "UserState");

            migrationBuilder.DropForeignKey(
                name: "FK_UserState_UserPCConstituency_UserPCConstituencyId",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserState_UserDistrictId",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserState_UserPCConstituencyId",
                table: "UserState");

            migrationBuilder.DropIndex(
                name: "IX_UserPCConstituency_UserAssemblyId",
                table: "UserPCConstituency");

            migrationBuilder.DropIndex(
                name: "IX_UserDistrict_UserAssemblyId",
                table: "UserDistrict");

            migrationBuilder.DropColumn(
                name: "UserDistrictId",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "UserPCConstituencyId",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "UserAssemblyId",
                table: "UserPCConstituency");

            migrationBuilder.DropColumn(
                name: "UserAssemblyId",
                table: "UserDistrict");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegRelationkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssembly_UserDistrict_UserDistrictId",
                table: "UserAssembly");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssembly_UserPCConstituency_UserPCConstituencyId",
                table: "UserAssembly");

            migrationBuilder.AlterColumn<int>(
                name: "UserPCConstituencyId",
                table: "UserAssembly",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserDistrictId",
                table: "UserAssembly",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssembly_UserDistrict_UserDistrictId",
                table: "UserAssembly",
                column: "UserDistrictId",
                principalTable: "UserDistrict",
                principalColumn: "UserDistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssembly_UserPCConstituency_UserPCConstituencyId",
                table: "UserAssembly",
                column: "UserPCConstituencyId",
                principalTable: "UserPCConstituency",
                principalColumn: "UserPCConstituencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssembly_UserDistrict_UserDistrictId",
                table: "UserAssembly");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssembly_UserPCConstituency_UserPCConstituencyId",
                table: "UserAssembly");

            migrationBuilder.AlterColumn<int>(
                name: "UserPCConstituencyId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserDistrictId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssembly_UserDistrict_UserDistrictId",
                table: "UserAssembly",
                column: "UserDistrictId",
                principalTable: "UserDistrict",
                principalColumn: "UserDistrictId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssembly_UserPCConstituency_UserPCConstituencyId",
                table: "UserAssembly",
                column: "UserPCConstituencyId",
                principalTable: "UserPCConstituency",
                principalColumn: "UserPCConstituencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

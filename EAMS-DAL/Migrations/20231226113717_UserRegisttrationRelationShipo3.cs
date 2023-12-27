using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegisttrationRelationShipo3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserState",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserPCConstituency",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserDistrict",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserAssembly",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserState",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserPCConstituency",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserDistrict",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserAssembly",
                newName: "UserId");
        }
    }
}

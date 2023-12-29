using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegRelationkeys : Migration
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
                name: "FK_UserDistrict_UserAssembly_UserAssemblyId",
                table: "UserDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPCConstituency_AspNetUsers_Id",
                table: "UserPCConstituency");

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
                name: "IX_UserPCConstituency_Id",
                table: "UserPCConstituency");

            migrationBuilder.DropIndex(
                name: "IX_UserDistrict_Id",
                table: "UserDistrict");

            migrationBuilder.DropIndex(
                name: "IX_UserAssembly_Id",
                table: "UserAssembly");

            migrationBuilder.DropColumn(
                name: "UserDistrictId",
                table: "UserState");

            migrationBuilder.DropColumn(
                name: "UserPCConstituencyId",
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

            migrationBuilder.RenameColumn(
                name: "UserAssemblyId",
                table: "UserPCConstituency",
                newName: "UserStateId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPCConstituency_UserAssemblyId",
                table: "UserPCConstituency",
                newName: "IX_UserPCConstituency_UserStateId");

            migrationBuilder.RenameColumn(
                name: "UserAssemblyId",
                table: "UserDistrict",
                newName: "UserStateId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDistrict_UserAssemblyId",
                table: "UserDistrict",
                newName: "IX_UserDistrict_UserStateId");

            migrationBuilder.AddColumn<int>(
                name: "UserDistrictId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserPCConstituencyId",
                table: "UserAssembly",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAssembly_UserDistrictId",
                table: "UserAssembly",
                column: "UserDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssembly_UserPCConstituencyId",
                table: "UserAssembly",
                column: "UserPCConstituencyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserDistrict_UserState_UserStateId",
                table: "UserDistrict",
                column: "UserStateId",
                principalTable: "UserState",
                principalColumn: "UserStateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPCConstituency_UserState_UserStateId",
                table: "UserPCConstituency",
                column: "UserStateId",
                principalTable: "UserState",
                principalColumn: "UserStateId",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_UserDistrict_UserState_UserStateId",
                table: "UserDistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPCConstituency_UserState_UserStateId",
                table: "UserPCConstituency");

            migrationBuilder.DropIndex(
                name: "IX_UserAssembly_UserDistrictId",
                table: "UserAssembly");

            migrationBuilder.DropIndex(
                name: "IX_UserAssembly_UserPCConstituencyId",
                table: "UserAssembly");

            migrationBuilder.DropColumn(
                name: "UserDistrictId",
                table: "UserAssembly");

            migrationBuilder.DropColumn(
                name: "UserPCConstituencyId",
                table: "UserAssembly");

            migrationBuilder.RenameColumn(
                name: "UserStateId",
                table: "UserPCConstituency",
                newName: "UserAssemblyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPCConstituency_UserStateId",
                table: "UserPCConstituency",
                newName: "IX_UserPCConstituency_UserAssemblyId");

            migrationBuilder.RenameColumn(
                name: "UserStateId",
                table: "UserDistrict",
                newName: "UserAssemblyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDistrict_UserStateId",
                table: "UserDistrict",
                newName: "IX_UserDistrict_UserAssemblyId");

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

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserPCConstituency",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserDistrict",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserAssembly",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserState_UserDistrictId",
                table: "UserState",
                column: "UserDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_UserState_UserPCConstituencyId",
                table: "UserState",
                column: "UserPCConstituencyId");

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
                name: "FK_UserDistrict_UserAssembly_UserAssemblyId",
                table: "UserDistrict",
                column: "UserAssemblyId",
                principalTable: "UserAssembly",
                principalColumn: "UserAssemblyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPCConstituency_AspNetUsers_Id",
                table: "UserPCConstituency",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
    }
}

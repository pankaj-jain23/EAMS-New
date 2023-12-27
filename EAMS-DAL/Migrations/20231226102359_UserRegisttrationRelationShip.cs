using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRegisttrationRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateMasterId",
                table: "AspNetUsers",
                newName: "UserStateId");

            migrationBuilder.RenameColumn(
                name: "DistrictMasterId",
                table: "AspNetUsers",
                newName: "UserPCConstituencyId");

            migrationBuilder.RenameColumn(
                name: "AssemblyMasterId",
                table: "AspNetUsers",
                newName: "UserDistrictId");

            migrationBuilder.AddColumn<int>(
                name: "UserAssemblyId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserAssembly",
                columns: table => new
                {
                    UserAssemblyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssembly", x => x.UserAssemblyId);
                });

            migrationBuilder.CreateTable(
                name: "UserDistrict",
                columns: table => new
                {
                    UserDistrictId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDistrict", x => x.UserDistrictId);
                });

            migrationBuilder.CreateTable(
                name: "UserPCConstituency",
                columns: table => new
                {
                    UserPCConstituencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PCMasterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPCConstituency", x => x.UserPCConstituencyId);
                });

            migrationBuilder.CreateTable(
                name: "UserState",
                columns: table => new
                {
                    UserStateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserState", x => x.UserStateId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "UserAssembly");

            migrationBuilder.DropTable(
                name: "UserDistrict");

            migrationBuilder.DropTable(
                name: "UserPCConstituency");

            migrationBuilder.DropTable(
                name: "UserState");

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

            migrationBuilder.RenameColumn(
                name: "UserStateId",
                table: "AspNetUsers",
                newName: "StateMasterId");

            migrationBuilder.RenameColumn(
                name: "UserPCConstituencyId",
                table: "AspNetUsers",
                newName: "DistrictMasterId");

            migrationBuilder.RenameColumn(
                name: "UserDistrictId",
                table: "AspNetUsers",
                newName: "AssemblyMasterId");
        }
    }
}

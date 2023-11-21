using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class somodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateMasterId",
                table: "AssemblyMaster",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyMaster_StateMasterId",
                table: "AssemblyMaster",
                column: "StateMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssemblyMaster_StateMaster_StateMasterId",
                table: "AssemblyMaster",
                column: "StateMasterId",
                principalTable: "StateMaster",
                principalColumn: "StateMasterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssemblyMaster_StateMaster_StateMasterId",
                table: "AssemblyMaster");

            migrationBuilder.DropIndex(
                name: "IX_AssemblyMaster_StateMasterId",
                table: "AssemblyMaster");

            migrationBuilder.DropColumn(
                name: "StateMasterId",
                table: "AssemblyMaster");
        }
    }
}

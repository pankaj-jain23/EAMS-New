using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class polldetailtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollDetail_SlotManagement_SlotManagementId",
                table: "PollDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlotManagement",
                table: "SlotManagement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PollDetail",
                table: "PollDetail");

            migrationBuilder.RenameTable(
                name: "SlotManagement",
                newName: "SlotManagementMaster");

            migrationBuilder.RenameTable(
                name: "PollDetail",
                newName: "PollDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PollDetail_SlotManagementId",
                table: "PollDetails",
                newName: "IX_PollDetails_SlotManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlotManagementMaster",
                table: "SlotManagementMaster",
                column: "SlotManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PollDetails",
                table: "PollDetails",
                column: "PollDetailMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollDetails_SlotManagementMaster_SlotManagementId",
                table: "PollDetails",
                column: "SlotManagementId",
                principalTable: "SlotManagementMaster",
                principalColumn: "SlotManagementId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollDetails_SlotManagementMaster_SlotManagementId",
                table: "PollDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlotManagementMaster",
                table: "SlotManagementMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PollDetails",
                table: "PollDetails");

            migrationBuilder.RenameTable(
                name: "SlotManagementMaster",
                newName: "SlotManagement");

            migrationBuilder.RenameTable(
                name: "PollDetails",
                newName: "PollDetail");

            migrationBuilder.RenameIndex(
                name: "IX_PollDetails_SlotManagementId",
                table: "PollDetail",
                newName: "IX_PollDetail_SlotManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlotManagement",
                table: "SlotManagement",
                column: "SlotManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PollDetail",
                table: "PollDetail",
                column: "PollDetailMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollDetail_SlotManagement_SlotManagementId",
                table: "PollDetail",
                column: "SlotManagementId",
                principalTable: "SlotManagement",
                principalColumn: "SlotManagementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

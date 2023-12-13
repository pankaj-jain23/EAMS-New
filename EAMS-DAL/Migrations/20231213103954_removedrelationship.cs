using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class removedrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollDetails_SlotManagementMaster_SlotManagementId",
                table: "PollDetails");

            migrationBuilder.DropIndex(
                name: "IX_PollDetails_SlotManagementId",
                table: "PollDetails");

            migrationBuilder.AlterColumn<int>(
                name: "SlotManagementId",
                table: "PollDetails",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SlotManagementId",
                table: "PollDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PollDetails_SlotManagementId",
                table: "PollDetails",
                column: "SlotManagementId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollDetails_SlotManagementMaster_SlotManagementId",
                table: "PollDetails",
                column: "SlotManagementId",
                principalTable: "SlotManagementMaster",
                principalColumn: "SlotManagementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

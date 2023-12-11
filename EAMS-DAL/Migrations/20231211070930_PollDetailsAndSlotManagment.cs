using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class PollDetailsAndSlotManagment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlotManagement",
                columns: table => new
                {
                    SlotManagementId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    EventMasterId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsLastSlot = table.Column<bool>(type: "boolean", nullable: false),
                    SlotCreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotManagement", x => x.SlotManagementId);
                });

            migrationBuilder.CreateTable(
                name: "PollDetail",
                columns: table => new
                {
                    PollDetailMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SlotManagementId = table.Column<int>(type: "integer", nullable: false),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false),
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: false),
                    BoothMasterId = table.Column<int>(type: "integer", nullable: false),
                    EventMasterId = table.Column<int>(type: "integer", nullable: false),
                    VotesPolled = table.Column<int>(type: "integer", nullable: false),
                    VotesPolledRecivedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AddedBy = table.Column<int>(type: "integer", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollDetail", x => x.PollDetailMasterId);
                    table.ForeignKey(
                        name: "FK_PollDetail_SlotManagement_SlotManagementId",
                        column: x => x.SlotManagementId,
                        principalTable: "SlotManagement",
                        principalColumn: "SlotManagementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollDetail_SlotManagementId",
                table: "PollDetail",
                column: "SlotManagementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollDetail");

            migrationBuilder.DropTable(
                name: "SlotManagement");
        }
    }
}

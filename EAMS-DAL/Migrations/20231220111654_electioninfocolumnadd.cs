using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class electioninfocolumnadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVoterTurnOut",
                table: "ElectionInfoMaster",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VotingTurnOutLastUpdate",
                table: "ElectionInfoMaster",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVoterTurnOut",
                table: "ElectionInfoMaster");

            migrationBuilder.DropColumn(
                name: "VotingTurnOutLastUpdate",
                table: "ElectionInfoMaster");
        }
    }
}

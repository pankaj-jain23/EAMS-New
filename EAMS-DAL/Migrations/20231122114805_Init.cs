using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EventMaster",
                newName: "EventMasterId");

            migrationBuilder.AddColumn<string>(
                name: "AssignedBy",
                table: "BoothMaster",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedOnTime",
                table: "BoothMaster",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "BoothMaster",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedBy",
                table: "BoothMaster");

            migrationBuilder.DropColumn(
                name: "AssignedOnTime",
                table: "BoothMaster");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "BoothMaster");

            migrationBuilder.RenameColumn(
                name: "EventMasterId",
                table: "EventMaster",
                newName: "Id");
        }
    }
}

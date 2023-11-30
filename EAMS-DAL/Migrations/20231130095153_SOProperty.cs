using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class SOProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "SectorOfficerMaster",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "SectorOfficerMaster",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTPAttempts",
                table: "SectorOfficerMaster",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpireTime",
                table: "SectorOfficerMaster",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPGeneratedTime",
                table: "SectorOfficerMaster",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "SectorOfficerMaster");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "SectorOfficerMaster");

            migrationBuilder.DropColumn(
                name: "OTPAttempts",
                table: "SectorOfficerMaster");

            migrationBuilder.DropColumn(
                name: "OTPExpireTime",
                table: "SectorOfficerMaster");

            migrationBuilder.DropColumn(
                name: "OTPGeneratedTime",
                table: "SectorOfficerMaster");
        }
    }
}

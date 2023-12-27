using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addfieldcollectioncentre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPartyReachedCollectionCenter",
                table: "ElectionInfoMaster",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PartyReachedCollectionCenterLastUpdate",
                table: "ElectionInfoMaster",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPartyReachedCollectionCenter",
                table: "ElectionInfoMaster");

            migrationBuilder.DropColumn(
                name: "PartyReachedCollectionCenterLastUpdate",
                table: "ElectionInfoMaster");
        }
    }
}

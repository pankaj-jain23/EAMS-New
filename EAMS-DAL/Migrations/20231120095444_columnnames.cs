using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class columnnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "StateMaster",
                newName: "StateUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "StateMaster",
                newName: "StateStatus");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StateMaster",
                newName: "StateName");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "StateMaster",
                newName: "StateDeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "StateMaster",
                newName: "StateCreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ParliamentConstituencyMaster",
                newName: "PcUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ParliamentConstituencyMaster",
                newName: "PcStatus");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "ParliamentConstituencyMaster",
                newName: "PcDeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ParliamentConstituencyMaster",
                newName: "PcCreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "DistrictMaster",
                newName: "DistrictUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DistrictMaster",
                newName: "DistrictStatus");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DistrictMaster",
                newName: "DistrictName");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "DistrictMaster",
                newName: "DistrictDeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "DistrictMaster",
                newName: "DistrictCreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "BoothMaster",
                newName: "BoothUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "BoothMaster",
                newName: "BoothStatus");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "BoothMaster",
                newName: "BoothDeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "BoothMaster",
                newName: "BoothCreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "AssemblyMaster",
                newName: "AssemblyUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AssemblyMaster",
                newName: "AssemblyStatus");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "AssemblyMaster",
                newName: "AssemblyDeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AssemblyMaster",
                newName: "AssemblyCreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateUpdatedAt",
                table: "StateMaster",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "StateStatus",
                table: "StateMaster",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StateName",
                table: "StateMaster",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StateDeletedAt",
                table: "StateMaster",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "StateCreatedAt",
                table: "StateMaster",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "PcUpdatedAt",
                table: "ParliamentConstituencyMaster",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "PcStatus",
                table: "ParliamentConstituencyMaster",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PcDeletedAt",
                table: "ParliamentConstituencyMaster",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "PcCreatedAt",
                table: "ParliamentConstituencyMaster",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DistrictUpdatedAt",
                table: "DistrictMaster",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DistrictStatus",
                table: "DistrictMaster",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DistrictName",
                table: "DistrictMaster",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DistrictDeletedAt",
                table: "DistrictMaster",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "DistrictCreatedAt",
                table: "DistrictMaster",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BoothUpdatedAt",
                table: "BoothMaster",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "BoothStatus",
                table: "BoothMaster",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "BoothDeletedAt",
                table: "BoothMaster",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "BoothCreatedAt",
                table: "BoothMaster",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AssemblyUpdatedAt",
                table: "AssemblyMaster",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "AssemblyStatus",
                table: "AssemblyMaster",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "AssemblyDeletedAt",
                table: "AssemblyMaster",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "AssemblyCreatedAt",
                table: "AssemblyMaster",
                newName: "CreatedAt");
        }
    }
}

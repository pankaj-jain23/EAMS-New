using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class somodeldbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SectorOfficerMaster",
                columns: table => new
                {
                    SOMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    SoName = table.Column<string>(type: "text", nullable: false),
                    SoDesignation = table.Column<string>(type: "text", nullable: false),
                    SoOfficeName = table.Column<string>(type: "text", nullable: false),
                    SoAssemblyCode = table.Column<int>(type: "integer", nullable: false),
                    SoMobile = table.Column<int>(type: "integer", nullable: false),
                    SoCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SOUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SoStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorOfficerMaster", x => x.SOMasterId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectorOfficerMaster");
        }
    }
}

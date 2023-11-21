using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Somechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventName = table.Column<string>(type: "text", nullable: false),
                    EventSequence = table.Column<int>(type: "integer", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateMaster",
                columns: table => new
                {
                    StateMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StateCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateMaster", x => x.StateMasterId);
                });

            migrationBuilder.CreateTable(
                name: "DistrictMaster",
                columns: table => new
                {
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DistrictCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictMaster", x => x.DistrictMasterId);
                    table.ForeignKey(
                        name: "FK_DistrictMaster_StateMaster_StateMasterId",
                        column: x => x.StateMasterId,
                        principalTable: "StateMaster",
                        principalColumn: "StateMasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParliamentConstituencyMaster",
                columns: table => new
                {
                    PCMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    PcCodeNo = table.Column<string>(type: "text", nullable: true),
                    PcName = table.Column<string>(type: "text", nullable: true),
                    PcType = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParliamentConstituencyMaster", x => x.PCMasterId);
                    table.ForeignKey(
                        name: "FK_ParliamentConstituencyMaster_StateMaster_StateMasterId",
                        column: x => x.StateMasterId,
                        principalTable: "StateMaster",
                        principalColumn: "StateMasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyMaster",
                columns: table => new
                {
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssemblyName = table.Column<string>(type: "text", nullable: false),
                    AssemblyCode = table.Column<int>(type: "integer", nullable: false),
                    AssemblyType = table.Column<string>(type: "text", nullable: true),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false),
                    PCMasterId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyMaster", x => x.AssemblyMasterId);
                    table.ForeignKey(
                        name: "FK_AssemblyMaster_DistrictMaster_DistrictMasterId",
                        column: x => x.DistrictMasterId,
                        principalTable: "DistrictMaster",
                        principalColumn: "DistrictMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyMaster_ParliamentConstituencyMaster_PCMasterId",
                        column: x => x.PCMasterId,
                        principalTable: "ParliamentConstituencyMaster",
                        principalColumn: "PCMasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoothMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false),
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: false),
                    BoothCode_No = table.Column<string>(type: "text", nullable: false),
                    TotalVoters = table.Column<int>(type: "integer", nullable: true),
                    BoothName = table.Column<string>(type: "text", nullable: true),
                    BoothNoAuxy = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoothMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoothMaster_AssemblyMaster_AssemblyMasterId",
                        column: x => x.AssemblyMasterId,
                        principalTable: "AssemblyMaster",
                        principalColumn: "AssemblyMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoothMaster_DistrictMaster_DistrictMasterId",
                        column: x => x.DistrictMasterId,
                        principalTable: "DistrictMaster",
                        principalColumn: "DistrictMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoothMaster_StateMaster_StateMasterId",
                        column: x => x.StateMasterId,
                        principalTable: "StateMaster",
                        principalColumn: "StateMasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyMaster_DistrictMasterId",
                table: "AssemblyMaster",
                column: "DistrictMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyMaster_PCMasterId",
                table: "AssemblyMaster",
                column: "PCMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BoothMaster_AssemblyMasterId",
                table: "BoothMaster",
                column: "AssemblyMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BoothMaster_DistrictMasterId",
                table: "BoothMaster",
                column: "DistrictMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_BoothMaster_StateMasterId",
                table: "BoothMaster",
                column: "StateMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMaster_StateMasterId",
                table: "DistrictMaster",
                column: "StateMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ParliamentConstituencyMaster_StateMasterId",
                table: "ParliamentConstituencyMaster",
                column: "StateMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoothMaster");

            migrationBuilder.DropTable(
                name: "EventMaster");

            migrationBuilder.DropTable(
                name: "AssemblyMaster");

            migrationBuilder.DropTable(
                name: "DistrictMaster");

            migrationBuilder.DropTable(
                name: "ParliamentConstituencyMaster");

            migrationBuilder.DropTable(
                name: "StateMaster");
        }
    }
}

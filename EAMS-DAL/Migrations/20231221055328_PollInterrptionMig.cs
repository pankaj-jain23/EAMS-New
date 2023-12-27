using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class PollInterrptionMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PollInterruptionHistory",
                columns: table => new
                {
                    PollInterruptionHisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false),
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: false),
                    BoothMasterId = table.Column<int>(type: "integer", nullable: false),
                    InterruptionType = table.Column<int>(type: "integer", nullable: false),
                    OldCU = table.Column<string>(type: "text", nullable: false),
                    OldBU = table.Column<string>(type: "text", nullable: false),
                    NewCU = table.Column<string>(type: "text", nullable: false),
                    NewBU = table.Column<string>(type: "text", nullable: false),
                    StopTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ResumeTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<string>(type: "text", nullable: false),
                    IsPollInterrupted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Flag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollInterruptionHistory", x => x.PollInterruptionHisId);
                });

            migrationBuilder.CreateTable(
                name: "PollInterruptions",
                columns: table => new
                {
                    PollInterruptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false),
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: false),
                    BoothMasterId = table.Column<int>(type: "integer", nullable: false),
                    InterruptionType = table.Column<int>(type: "integer", nullable: false),
                    OldCU = table.Column<string>(type: "text", nullable: false),
                    OldBU = table.Column<string>(type: "text", nullable: false),
                    NewCU = table.Column<string>(type: "text", nullable: false),
                    NewBU = table.Column<string>(type: "text", nullable: false),
                    StopTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ResumeTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<string>(type: "text", nullable: false),
                    IsPollInterrupted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Flag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollInterruptions", x => x.PollInterruptionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollInterruptionHistory");

            migrationBuilder.DropTable(
                name: "PollInterruptions");
        }
    }
}

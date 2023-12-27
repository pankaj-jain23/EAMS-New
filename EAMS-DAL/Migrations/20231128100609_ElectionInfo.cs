using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class ElectionInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectionInfoMaster",
                columns: table => new
                {
                    ElectionInfoMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssemblyMasterId = table.Column<int>(type: "integer", nullable: false),
                    EventMasterId = table.Column<int>(type: "integer", nullable: false),
                    StateMasterId = table.Column<int>(type: "integer", nullable: false),
                    DistrictMasterId = table.Column<int>(type: "integer", nullable: false),
                    BoothMasterId = table.Column<int>(type: "integer", nullable: false),
                    ElectionInfoCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ElectionInfoUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ElectionInfoDeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ElectionInfoStatus = table.Column<bool>(type: "boolean", nullable: true),
                    SOUserId = table.Column<string>(type: "text", nullable: true),
                    AROUserId = table.Column<string>(type: "text", nullable: true),
                    IsPartyDispatched = table.Column<bool>(type: "boolean", nullable: true),
                    PartyDispatchedLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsPartyReached = table.Column<bool>(type: "boolean", nullable: true),
                    PartyReachedLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsSetupOfPolling = table.Column<bool>(type: "boolean", nullable: true),
                    SetupOfPollingLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsMockPollDone = table.Column<bool>(type: "boolean", nullable: true),
                    MockPollDoneLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsPollStarted = table.Column<bool>(type: "boolean", nullable: true),
                    PollStartedLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalTVote = table.Column<int>(type: "integer", nullable: true),
                    VotingLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VoterInQueue = table.Column<int>(type: "integer", nullable: true),
                    VoterInQueueLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsPollEnded = table.Column<bool>(type: "boolean", nullable: true),
                    IsPollEndedLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsMCESwitchOff = table.Column<bool>(type: "boolean", nullable: true),
                    MCESwitchOffLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsPartyDeparted = table.Column<bool>(type: "boolean", nullable: true),
                    PartyDepartedLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsEVMDeposited = table.Column<bool>(type: "boolean", nullable: true),
                    EVMDepositedLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionInfoMaster", x => x.ElectionInfoMasterId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionInfoMaster");
        }
    }
}

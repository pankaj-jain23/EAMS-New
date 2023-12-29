using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class smstemplateandsmssentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSTemplate",
                columns: table => new
                {
                    SMSTemplateMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<string>(type: "text", nullable: true),
                    TemplateId = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSTemplate", x => x.SMSTemplateMasterId);
                });

            migrationBuilder.CreateTable(
                name: "SMSSent",
                columns: table => new
                {
                    SMSSentMasterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SMSTemplateMasterId = table.Column<int>(type: "integer", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    RemarksFromGW = table.Column<string>(type: "text", nullable: true),
                    SentToUserType = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSSent", x => x.SMSSentMasterId);
                    table.ForeignKey(
                        name: "FK_SMSSent_SMSTemplate_SMSTemplateMasterId",
                        column: x => x.SMSTemplateMasterId,
                        principalTable: "SMSTemplate",
                        principalColumn: "SMSTemplateMasterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSSent_SMSTemplateMasterId",
                table: "SMSSent",
                column: "SMSTemplateMasterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSSent");

            migrationBuilder.DropTable(
                name: "SMSTemplate");
        }
    }
}

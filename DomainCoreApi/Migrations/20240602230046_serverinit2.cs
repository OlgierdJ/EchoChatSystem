using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class serverinit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerEventParticipancy",
                columns: table => new
                {
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EventId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeJoined = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerEventParticipancy", x => new { x.ServerId, x.EventId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_ServerEventParticipancy_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerEventParticipancy_ServerEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "ServerEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerEventParticipancy_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                    table.ForeignKey(
                        name: "FK_ServerEventParticipancy_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerEventParticipancy_AccountId_ServerId",
                table: "ServerEventParticipancy",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerEventParticipancy_EventId",
                table: "ServerEventParticipancy",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerEventParticipancy");
        }
    }
}

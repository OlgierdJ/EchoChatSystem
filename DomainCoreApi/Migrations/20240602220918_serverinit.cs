using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class serverinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerInvite_ServerTextChannel_ChannelId",
                table: "ServerInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerVoiceInvite_ServerVoiceChannel_ChannelId",
                table: "ServerVoiceInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerVoiceInvite_Server_SubjectId",
                table: "ServerVoiceInvite");

            migrationBuilder.DropTable(
                name: "ServerTextChannelAccountMessageTracker");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMute");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "ServerInvite",
                newName: "VoiceChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_ServerInvite_ChannelId",
                table: "ServerInvite",
                newName: "IX_ServerInvite_VoiceChannelId");

            migrationBuilder.AddColumn<byte>(
                name: "Volume",
                table: "ServerSoundboardSound",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "GuestInvite",
                table: "ServerInvite",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TextChannelId",
                table: "ServerInvite",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountServerTextChannelMessageTracker",
                columns: table => new
                {
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CoOwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerTextChannelMessageTracker", x => new { x.OwnerId, x.CoOwnerId });
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMessageTracker_Account_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMessageTracker_ServerTextChannelMessage_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerTextChannelMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMessageTracker_ServerTextChannel_CoOwnerId",
                        column: x => x.CoOwnerId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountServerTextChannelMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerTextChannelMute", x => new { x.MuterId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMute_ServerTextChannel_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerInvite_TextChannelId",
                table: "ServerInvite",
                column: "TextChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMessageTracker_CoOwnerId",
                table: "AccountServerTextChannelMessageTracker",
                column: "CoOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMessageTracker_SubjectId",
                table: "AccountServerTextChannelMessageTracker",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMute_SubjectId",
                table: "AccountServerTextChannelMute",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerInvite_ServerTextChannel_TextChannelId",
                table: "ServerInvite",
                column: "TextChannelId",
                principalTable: "ServerTextChannel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerInvite_ServerVoiceChannel_VoiceChannelId",
                table: "ServerInvite",
                column: "VoiceChannelId",
                principalTable: "ServerVoiceChannel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerVoiceInvite_ServerVoiceChannel_ChannelId",
                table: "ServerVoiceInvite",
                column: "ChannelId",
                principalTable: "ServerVoiceChannel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerVoiceInvite_Server_SubjectId",
                table: "ServerVoiceInvite",
                column: "SubjectId",
                principalTable: "Server",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerInvite_ServerTextChannel_TextChannelId",
                table: "ServerInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerInvite_ServerVoiceChannel_VoiceChannelId",
                table: "ServerInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerVoiceInvite_ServerVoiceChannel_ChannelId",
                table: "ServerVoiceInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerVoiceInvite_Server_SubjectId",
                table: "ServerVoiceInvite");

            migrationBuilder.DropTable(
                name: "AccountServerTextChannelMessageTracker");

            migrationBuilder.DropTable(
                name: "AccountServerTextChannelMute");

            migrationBuilder.DropIndex(
                name: "IX_ServerInvite_TextChannelId",
                table: "ServerInvite");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "ServerSoundboardSound");

            migrationBuilder.DropColumn(
                name: "GuestInvite",
                table: "ServerInvite");

            migrationBuilder.DropColumn(
                name: "TextChannelId",
                table: "ServerInvite");

            migrationBuilder.RenameColumn(
                name: "VoiceChannelId",
                table: "ServerInvite",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_ServerInvite_VoiceChannelId",
                table: "ServerInvite",
                newName: "IX_ServerInvite_ChannelId");

            migrationBuilder.CreateTable(
                name: "ServerTextChannelAccountMessageTracker",
                columns: table => new
                {
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CoOwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelAccountMessageTracker", x => new { x.OwnerId, x.CoOwnerId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelAccountMessageTracker_Account_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelAccountMessageTracker_ServerTextChannelMessage_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerTextChannelMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelAccountMessageTracker_ServerTextChannel_CoOwnerId",
                        column: x => x.CoOwnerId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMute",
                columns: table => new
                {
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelMute", x => new { x.MuterId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMute_ServerTextChannel_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelAccountMessageTracker_CoOwnerId",
                table: "ServerTextChannelAccountMessageTracker",
                column: "CoOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelAccountMessageTracker_SubjectId",
                table: "ServerTextChannelAccountMessageTracker",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMute_SubjectId",
                table: "ServerTextChannelMute",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerInvite_ServerTextChannel_ChannelId",
                table: "ServerInvite",
                column: "ChannelId",
                principalTable: "ServerTextChannel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerVoiceInvite_ServerVoiceChannel_ChannelId",
                table: "ServerVoiceInvite",
                column: "ChannelId",
                principalTable: "ServerVoiceChannel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerVoiceInvite_Server_SubjectId",
                table: "ServerVoiceInvite",
                column: "SubjectId",
                principalTable: "Server",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

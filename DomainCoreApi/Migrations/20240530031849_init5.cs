using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAccountVolume_Account_OwnerId",
                table: "AccountAccountVolume");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlock_Account_BlockerId",
                table: "AccountBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSettings_Account_Id",
                table: "AccountSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMute_Chat_ChatId",
                table: "ChatMute");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerTextChannelMessage_ServerTextChannelMessage_ParentId",
                table: "ServerTextChannelMessage");

            migrationBuilder.DropTable(
                name: "AccountServerTextChannelMute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServerTextChannelAccountMessageTracker",
                table: "ServerTextChannelAccountMessageTracker");

            migrationBuilder.DropIndex(
                name: "IX_ChatMute_ChatId",
                table: "ChatMute");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "ChatMute");

            migrationBuilder.AlterColumn<string>(
                name: "FileLocationURL",
                table: "ServerTextChannelMessageAttachment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthorId",
                table: "ServerTextChannelMessage",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerTextChannelAccountMessageTracker",
                table: "ServerTextChannelAccountMessageTracker",
                columns: new[] { "OwnerId", "CoOwnerId" });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "IX_ServerTextChannelMute_SubjectId",
                table: "ServerTextChannelMute",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAccountVolume_Account_OwnerId",
                table: "AccountAccountVolume",
                column: "OwnerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlock_Account_BlockerId",
                table: "AccountBlock",
                column: "BlockerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSettings_Account_Id",
                table: "AccountSettings",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTextChannelMessage_ServerTextChannelMessage_ParentId",
                table: "ServerTextChannelMessage",
                column: "ParentId",
                principalTable: "ServerTextChannelMessage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAccountVolume_Account_OwnerId",
                table: "AccountAccountVolume");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlock_Account_BlockerId",
                table: "AccountBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSettings_Account_Id",
                table: "AccountSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerTextChannelMessage_ServerTextChannelMessage_ParentId",
                table: "ServerTextChannelMessage");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServerTextChannelAccountMessageTracker",
                table: "ServerTextChannelAccountMessageTracker");

            migrationBuilder.AlterColumn<string>(
                name: "FileLocationURL",
                table: "ServerTextChannelMessageAttachment",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AuthorId",
                table: "ServerTextChannelMessage",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChatId",
                table: "ChatMute",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerTextChannelAccountMessageTracker",
                table: "ServerTextChannelAccountMessageTracker",
                columns: new[] { "OwnerId", "CoOwnerId", "SubjectId" });

            migrationBuilder.CreateTable(
                name: "AccountServerTextChannelMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerTextChannelMute", x => new { x.SubjectId, x.MuterId });
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMute_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
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
                name: "IX_ChatMute_ChatId",
                table: "ChatMute",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMute_AccountId",
                table: "AccountServerTextChannelMute",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMute_MuterId",
                table: "AccountServerTextChannelMute",
                column: "MuterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAccountVolume_Account_OwnerId",
                table: "AccountAccountVolume",
                column: "OwnerId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlock_Account_BlockerId",
                table: "AccountBlock",
                column: "BlockerId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSettings_Account_Id",
                table: "AccountSettings",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMute_Chat_ChatId",
                table: "ChatMute",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerTextChannelMessage_ServerTextChannelMessage_ParentId",
                table: "ServerTextChannelMessage",
                column: "ParentId",
                principalTable: "ServerTextChannelMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

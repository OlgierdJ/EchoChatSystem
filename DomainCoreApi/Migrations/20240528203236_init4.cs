using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipancy_Account_AccountId",
                table: "ChatParticipancy");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipancy_Chat_ChatId",
                table: "ChatParticipancy");

            migrationBuilder.DropIndex(
                name: "IX_ChatParticipancy_AccountId",
                table: "ChatParticipancy");

            migrationBuilder.DropIndex(
                name: "IX_ChatParticipancy_ChatId",
                table: "ChatParticipancy");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ChatParticipancy");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "ChatParticipancy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountId",
                table: "ChatParticipancy",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChatId",
                table: "ChatParticipancy",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipancy_AccountId",
                table: "ChatParticipancy",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipancy_ChatId",
                table: "ChatParticipancy",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipancy_Account_AccountId",
                table: "ChatParticipancy",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipancy_Chat_ChatId",
                table: "ChatParticipancy",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");
        }
    }
}

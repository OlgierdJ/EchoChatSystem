using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipParticipancy_Account_AccountId",
                table: "FriendshipParticipancy");

            migrationBuilder.DropIndex(
                name: "IX_FriendshipParticipancy_AccountId",
                table: "FriendshipParticipancy");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "FriendshipParticipancy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountId",
                table: "FriendshipParticipancy",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipParticipancy_AccountId",
                table: "FriendshipParticipancy",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipParticipancy_Account_AccountId",
                table: "FriendshipParticipancy",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}

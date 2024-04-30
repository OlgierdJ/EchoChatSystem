using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipParticipancy_Account_ParticipantId",
                table: "FriendshipParticipancy");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipParticipancy_Friendship_SubjectId",
                table: "FriendshipParticipancy");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipParticipancy_Account_ParticipantId",
                table: "FriendshipParticipancy",
                column: "ParticipantId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipParticipancy_Friendship_SubjectId",
                table: "FriendshipParticipancy",
                column: "SubjectId",
                principalTable: "Friendship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipParticipancy_Account_ParticipantId",
                table: "FriendshipParticipancy");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipParticipancy_Friendship_SubjectId",
                table: "FriendshipParticipancy");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipParticipancy_Account_ParticipantId",
                table: "FriendshipParticipancy",
                column: "ParticipantId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipParticipancy_Friendship_SubjectId",
                table: "FriendshipParticipancy",
                column: "SubjectId",
                principalTable: "Friendship",
                principalColumn: "Id");
        }
    }
}

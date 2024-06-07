using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class serverinvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerInvite_Account_AccountId",
                table: "ServerInvite");

            migrationBuilder.DropIndex(
                name: "IX_ServerInvite_AccountId",
                table: "ServerInvite");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ServerInvite");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountId",
                table: "ServerInvite",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerInvite_AccountId",
                table: "ServerInvite",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerInvite_Account_AccountId",
                table: "ServerInvite",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}

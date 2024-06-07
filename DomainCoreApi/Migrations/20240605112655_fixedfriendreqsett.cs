using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class fixedfriendreqsett : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountSettingsId",
                table: "FriendRequestSettings");

            migrationBuilder.RenameColumn(
                name: "SessionToken",
                table: "AccountSession",
                newName: "AccessToken");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AccountSession",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AccountSession");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "AccountSession",
                newName: "SessionToken");

            migrationBuilder.AddColumn<decimal>(
                name: "AccountSettingsId",
                table: "FriendRequestSettings",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

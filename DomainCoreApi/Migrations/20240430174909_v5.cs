using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatAccountMessageTracker_Chat_HolderId",
                table: "ChatAccountMessageTracker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatAccountMessageTracker",
                table: "ChatAccountMessageTracker");

            migrationBuilder.DropIndex(
                name: "IX_ChatAccountMessageTracker_OwnerId",
                table: "ChatAccountMessageTracker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatAccountMessageTracker");

            migrationBuilder.RenameColumn(
                name: "HolderId",
                table: "ChatAccountMessageTracker",
                newName: "CoOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatAccountMessageTracker_HolderId",
                table: "ChatAccountMessageTracker",
                newName: "IX_ChatAccountMessageTracker_CoOwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatAccountMessageTracker",
                table: "ChatAccountMessageTracker",
                columns: new[] { "OwnerId", "CoOwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatAccountMessageTracker_Chat_CoOwnerId",
                table: "ChatAccountMessageTracker",
                column: "CoOwnerId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatAccountMessageTracker_Chat_CoOwnerId",
                table: "ChatAccountMessageTracker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatAccountMessageTracker",
                table: "ChatAccountMessageTracker");

            migrationBuilder.RenameColumn(
                name: "CoOwnerId",
                table: "ChatAccountMessageTracker",
                newName: "HolderId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatAccountMessageTracker_CoOwnerId",
                table: "ChatAccountMessageTracker",
                newName: "IX_ChatAccountMessageTracker_HolderId");

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "ChatAccountMessageTracker",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatAccountMessageTracker",
                table: "ChatAccountMessageTracker",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAccountMessageTracker_OwnerId",
                table: "ChatAccountMessageTracker",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatAccountMessageTracker_Chat_HolderId",
                table: "ChatAccountMessageTracker",
                column: "HolderId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

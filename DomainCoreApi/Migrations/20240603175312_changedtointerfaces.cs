using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class changedtointerfaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugReport_Account_ReporterId",
                table: "BugReport");

            migrationBuilder.RenameColumn(
                name: "ReporterId",
                table: "BugReport",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BugReport_ReporterId",
                table: "BugReport",
                newName: "IX_BugReport_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BugReport_Account_AuthorId",
                table: "BugReport",
                column: "AuthorId",
                principalTable: "Account",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugReport_Account_AuthorId",
                table: "BugReport");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "BugReport",
                newName: "ReporterId");

            migrationBuilder.RenameIndex(
                name: "IX_BugReport_AuthorId",
                table: "BugReport",
                newName: "IX_BugReport_ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BugReport_Account_ReporterId",
                table: "BugReport",
                column: "ReporterId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}

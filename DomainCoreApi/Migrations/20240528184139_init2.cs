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
                name: "FK_AccountRole_Account_AccountId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Account_AccountId",
                table: "AccountRole",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Account_AccountId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Account_AccountId",
                table: "AccountRole",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AccountRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "AccountRole",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

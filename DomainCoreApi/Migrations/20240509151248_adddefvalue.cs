using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class adddefvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountActivityStatus",
                columns: new[] { "Id", "Description", "Icon", "IconColor", "Name" },
                values: new object[,]
                {
                    { (byte)1, "", "Icons.Material.Filled.Circle", "Success", "Online" },
                    { (byte)2, "", "Icons.Material.Filled.Brightness2", "Warning", "Away" },
                    { (byte)3, "You will not receive any desktop notifications.", "Icons.Material.Filled.RemoveCircle", "Error", "Busy" },
                    { (byte)4, "", "Icons.Material.Filled.StopCircle", "Dark", "Offline" },
                    { (byte)5, "You will not appear online, but have full access to all of Echo.", "Icons.Material.Filled.StopCircle", "Dark", "Invisible" }
                });

            migrationBuilder.InsertData(
                table: "Theme",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Dark" },
                    { 2L, "Light" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountActivityStatus",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "AccountActivityStatus",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "AccountActivityStatus",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "AccountActivityStatus",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.DeleteData(
                table: "AccountActivityStatus",
                keyColumn: "Id",
                keyValue: (byte)5);

            migrationBuilder.DeleteData(
                table: "Theme",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Theme",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}

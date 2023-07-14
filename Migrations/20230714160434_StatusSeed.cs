using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hltb.Migrations
{
    /// <inheritdoc />
    public partial class StatusSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "completed" },
                    { 2L, "backlog" },
                    { 3L, "retired" },
                    { 4L, "in progress" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "statuses",
                keyColumn: "id",
                keyValue: 4L);
        }
    }
}

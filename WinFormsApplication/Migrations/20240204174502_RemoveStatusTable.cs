using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace hltb.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(name: "status", table: "games", type: "text", nullable: false, defaultValue: "backlog");
            migrationBuilder.AddColumn<string>(name: "status", table: "films", type: "text", nullable: false, defaultValue: "backlog");
            migrationBuilder.Sql("UPDATE public.games SET status = (SELECT NAME FROM public.statuses WHERE statuses.id = games.status_id)");
            migrationBuilder.Sql("UPDATE public.films SET status = (SELECT NAME FROM public.statuses WHERE statuses.id = films.status_id)");
            migrationBuilder.DropColumn(name: "status_id", table: "games");
            migrationBuilder.DropColumn(name: "status_id", table: "films");
            migrationBuilder.DropForeignKey(name: "status", table: "games");
            migrationBuilder.DropForeignKey(name: "status", table: "films");
            migrationBuilder.DropTable(name: "statuses");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statuses_pkey", x => x.id);
                });
            migrationBuilder.AddColumn<long>(name: "status_id", table: "games", type: "bigint", nullable: false);
            migrationBuilder.AddColumn<long>(name: "status_id", table: "films", type: "bigint", nullable: false);
            migrationBuilder.CreateIndex(
                name: "IX_films_status_id",
                table: "films",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_games_status_id",
                table: "games",
                column: "status_id");
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
            
            migrationBuilder.AddForeignKey(name: "status,", table: "games", column: "status_id", principalTable: "statuses");
            migrationBuilder.AddForeignKey(name: "status,", table: "films", column: "status_id", principalTable: "statuses");
            

        }
    }
}

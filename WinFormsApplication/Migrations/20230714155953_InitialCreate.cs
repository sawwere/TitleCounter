using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace hltb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "films",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rus_title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true, defaultValueSql: "'None'::character varying"),
                    title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false, defaultValueSql: "'None'::character varying"),
                    fixed_title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false, defaultValueSql: "'None'::character varying"),
                    image_url = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'https://kitairu.net/images/noimage.png'::text"),
                    link_url = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'https://howlongtobeat.com'::text"),
                    time = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "0"),
                    status_id = table.Column<long>(type: "bigint", nullable: false),
                    date_release = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "'1900-01-01'::date"),
                    date_completed = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "'1900-01-01'::date"),
                    note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    score = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("films_pkey", x => x.id);
                    table.ForeignKey(
                        name: "status",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    platform = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true),
                    title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false, defaultValueSql: "'None'::character varying"),
                    fixed_title = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false, defaultValueSql: "'None'::character varying"),
                    image_url = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'https://kitairu.net/images/noimage.png'::text"),
                    link_url = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'https://howlongtobeat.com'::text"),
                    time = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "0"),
                    status_id = table.Column<long>(type: "bigint", nullable: false),
                    date_release = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "'1900-01-01'::date"),
                    date_completed = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "'1900-01-01'::date"),
                    note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    score = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("games_pkey", x => x.id);
                    table.ForeignKey(
                        name: "status",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_status_id",
                table: "films",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_games_status_id",
                table: "games",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "films");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}

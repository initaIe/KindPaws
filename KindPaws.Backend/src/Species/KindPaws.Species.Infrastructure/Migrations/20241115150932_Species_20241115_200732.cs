using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Species.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Species_20241115_200732 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "species");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "species",
                schema: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "citext", maxLength: 64, nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    soft_delete_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                schema: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "citext", maxLength: 64, nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    soft_delete_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    specie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                    table.ForeignKey(
                        name: "fk_breeds_species_specie_id",
                        column: x => x.specie_id,
                        principalSchema: "species",
                        principalTable: "species",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_breeds_name",
                schema: "species",
                table: "breeds",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_breeds_specie_id",
                schema: "species",
                table: "breeds",
                column: "specie_id");

            migrationBuilder.CreateIndex(
                name: "ix_species_name",
                schema: "species",
                table: "species",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds",
                schema: "species");

            migrationBuilder.DropTable(
                name: "species",
                schema: "species");
        }
    }
}

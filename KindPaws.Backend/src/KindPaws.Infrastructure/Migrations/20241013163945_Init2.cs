using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "health_details",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "diseases",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "healthStatus",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_neutered",
                table: "pets",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vaccines",
                table: "pets",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diseases",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "healthStatus",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_neutered",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "vaccines",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "health_details",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}

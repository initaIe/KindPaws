using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "species");

            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "breeds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "breeds",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

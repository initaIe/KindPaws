using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "volunteers",
                newName: "is_soft_deleted");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "species",
                newName: "is_soft_deleted");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "pets",
                newName: "is_soft_deleted");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "breeds",
                newName: "is_soft_deleted");

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "soft_delete_datetime",
                table: "volunteers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "soft_delete_datetime",
                table: "species",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "soft_delete_datetime",
                table: "pets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_hard_deleted",
                table: "breeds",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "soft_delete_datetime",
                table: "breeds",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "soft_delete_datetime",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "species");

            migrationBuilder.DropColumn(
                name: "soft_delete_datetime",
                table: "species");

            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "soft_delete_datetime",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_hard_deleted",
                table: "breeds");

            migrationBuilder.DropColumn(
                name: "soft_delete_datetime",
                table: "breeds");

            migrationBuilder.RenameColumn(
                name: "is_soft_deleted",
                table: "volunteers",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "is_soft_deleted",
                table: "species",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "is_soft_deleted",
                table: "pets",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "is_soft_deleted",
                table: "breeds",
                newName: "is_deleted");
        }
    }
}

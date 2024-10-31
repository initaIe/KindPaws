using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id1",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_volunteer_id1",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "volunteer_id1",
                table: "pets");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets");

            migrationBuilder.AddColumn<Guid>(
                name: "volunteer_id1",
                table: "pets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id1",
                table: "pets",
                column: "volunteer_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id1",
                table: "pets",
                column: "volunteer_id1",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breeds_species_specie_id",
                table: "breeds");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_breeds_breed_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pets",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_breed_id",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_specie_id",
                table: "pets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_breeds",
                table: "breeds");

            migrationBuilder.RenameTable(
                name: "pets",
                newName: "pet");

            migrationBuilder.RenameTable(
                name: "breeds",
                newName: "breed");

            migrationBuilder.RenameIndex(
                name: "ix_pets_volunteer_id",
                table: "pet",
                newName: "ix_pet_volunteer_id");

            migrationBuilder.RenameIndex(
                name: "ix_breeds_specie_id",
                table: "breed",
                newName: "ix_breed_specie_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "volunteer_id",
                table: "pet",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "specie_id",
                table: "breed",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_pet",
                table: "pet",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_breed",
                table: "breed",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_breed_species_specie_id",
                table: "breed",
                column: "specie_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pet_volunteers_volunteer_id",
                table: "pet",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breed_species_specie_id",
                table: "breed");

            migrationBuilder.DropForeignKey(
                name: "fk_pet_volunteers_volunteer_id",
                table: "pet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pet",
                table: "pet");

            migrationBuilder.DropPrimaryKey(
                name: "pk_breed",
                table: "breed");

            migrationBuilder.RenameTable(
                name: "pet",
                newName: "pets");

            migrationBuilder.RenameTable(
                name: "breed",
                newName: "breeds");

            migrationBuilder.RenameIndex(
                name: "ix_pet_volunteer_id",
                table: "pets",
                newName: "ix_pets_volunteer_id");

            migrationBuilder.RenameIndex(
                name: "ix_breed_specie_id",
                table: "breeds",
                newName: "ix_breeds_specie_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "volunteer_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "specie_id",
                table: "breeds",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_pets",
                table: "pets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_breeds",
                table: "breeds",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_breed_id",
                table: "pets",
                column: "breed_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_specie_id",
                table: "pets",
                column: "specie_id");

            migrationBuilder.AddForeignKey(
                name: "fk_breeds_species_specie_id",
                table: "breeds",
                column: "specie_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_breeds_breed_id",
                table: "pets",
                column: "breed_id",
                principalTable: "breeds",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets",
                column: "specie_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

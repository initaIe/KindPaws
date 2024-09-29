using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTablesAndConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "age_date_birth",
                table: "pets",
                newName: "date_birth");

            migrationBuilder.RenameColumn(
                name: "address_street",
                table: "pets",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "address_country",
                table: "pets",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "address_city",
                table: "pets",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "characteristics_details_weight",
                table: "pets",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "characteristics_details_height",
                table: "pets",
                newName: "height");

            migrationBuilder.RenameColumn(
                name: "characteristics_details_gender_value",
                table: "pets",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "PhotosDetails",
                table: "pets",
                newName: "photos_details");

            migrationBuilder.RenameColumn(
                name: "HelpDetails",
                table: "pets",
                newName: "help_details");

            migrationBuilder.RenameColumn(
                name: "HealthDetails",
                table: "pets",
                newName: "health_details");

            migrationBuilder.AddColumn<Guid>(
                name: "breed_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "specie_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "volunteer_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    experience = table.Column<int>(type: "integer", nullable: false),
                    email_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    social_networks = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    specie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    colors = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                    table.ForeignKey(
                        name: "fk_breeds_species_specie_id",
                        column: x => x.specie_id,
                        principalTable: "species",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_breed_id",
                table: "pets",
                column: "breed_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_specie_id",
                table: "pets",
                column: "specie_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_breeds_specie_id",
                table: "breeds",
                column: "specie_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_breeds_breed_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_species_specie_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets");

            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "volunteers");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropIndex(
                name: "ix_pets_breed_id",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_specie_id",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_volunteer_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "breed_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "color",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "specie_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "volunteer_id",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "pets",
                newName: "address_street");

            migrationBuilder.RenameColumn(
                name: "date_birth",
                table: "pets",
                newName: "age_date_birth");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "pets",
                newName: "address_country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "pets",
                newName: "address_city");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "pets",
                newName: "characteristics_details_weight");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "pets",
                newName: "characteristics_details_height");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "pets",
                newName: "characteristics_details_gender_value");

            migrationBuilder.RenameColumn(
                name: "photos_details",
                table: "pets",
                newName: "PhotosDetails");

            migrationBuilder.RenameColumn(
                name: "help_details",
                table: "pets",
                newName: "HelpDetails");

            migrationBuilder.RenameColumn(
                name: "health_details",
                table: "pets",
                newName: "HealthDetails");
        }
    }
}

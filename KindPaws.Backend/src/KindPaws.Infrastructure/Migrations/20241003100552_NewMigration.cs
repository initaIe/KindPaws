using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthDetails",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "address_city",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "address_country",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "address_street",
                table: "pets");

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
                newName: "support_status");

            migrationBuilder.RenameColumn(
                name: "age_date_birth",
                table: "pets",
                newName: "date_birth");

            migrationBuilder.RenameColumn(
                name: "PhotosDetails",
                table: "pets",
                newName: "photos_details");

            migrationBuilder.RenameColumn(
                name: "HelpDetails",
                table: "pets",
                newName: "health_details");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "pets",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "pets",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<float>(
                name: "weight",
                table: "pets",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "height",
                table: "pets",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "support_status",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date_birth",
                table: "pets",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<Guid>(
                name: "breed_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "pets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

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
                nullable: true);

            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
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
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    email_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    experience = table.Column<int>(type: "integer", nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    requisites = table.Column<string>(type: "jsonb", nullable: false),
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
                    specie_id = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
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
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_breeds_specie_id",
                table: "breeds",
                column: "specie_id");

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

            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "volunteers");

            migrationBuilder.DropTable(
                name: "species");

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
                name: "gender",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "specie_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "volunteer_id",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "pets",
                newName: "characteristics_details_weight");

            migrationBuilder.RenameColumn(
                name: "support_status",
                table: "pets",
                newName: "characteristics_details_gender_value");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "pets",
                newName: "characteristics_details_height");

            migrationBuilder.RenameColumn(
                name: "date_birth",
                table: "pets",
                newName: "age_date_birth");

            migrationBuilder.RenameColumn(
                name: "photos_details",
                table: "pets",
                newName: "PhotosDetails");

            migrationBuilder.RenameColumn(
                name: "health_details",
                table: "pets",
                newName: "HelpDetails");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "pets",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "characteristics_details_weight",
                table: "pets",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "characteristics_details_gender_value",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "characteristics_details_height",
                table: "pets",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "age_date_birth",
                table: "pets",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthDetails",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_city",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_country",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_street",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

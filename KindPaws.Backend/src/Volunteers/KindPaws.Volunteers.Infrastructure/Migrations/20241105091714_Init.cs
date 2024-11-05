using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindPaws.Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "volunteers");

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS citext;");
            
            migrationBuilder.CreateTable(
                name: "volunteers",
                schema: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    address = table.Column<string>(type: "jsonb", nullable: true),
                    years_of_experience = table.Column<int>(type: "integer", nullable: true),
                    social_networks = table.Column<string>(type: "jsonb", nullable: false),
                    requisites = table.Column<string>(type: "jsonb", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    soft_delete_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email_address = table.Column<string>(type: "citext", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "citext", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "citext", maxLength: 100, nullable: false),
                    patronymic = table.Column<string>(type: "citext", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                schema: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    support_status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    date_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    health_details = table.Column<string>(type: "jsonb", nullable: false),
                    biometric_details = table.Column<string>(type: "jsonb", nullable: false),
                    photos = table.Column<string>(type: "jsonb", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    soft_delete_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "citext", maxLength: 50, nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalSchema: "volunteers",
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                schema: "volunteers",
                table: "pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets",
                schema: "volunteers");

            migrationBuilder.DropTable(
                name: "volunteers",
                schema: "volunteers");
            
            migrationBuilder.Sql("DROP EXTENSION IF EXISTS citext;");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOEF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProducerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Continent",
                table: "Producers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstYear",
                table: "Producers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Continent",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "EstYear",
                table: "Producers");
        }
    }
}

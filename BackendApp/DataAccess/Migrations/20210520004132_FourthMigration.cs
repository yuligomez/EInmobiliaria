using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number_location",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Street_location",
                table: "Apartments");

            migrationBuilder.AddColumn<int>(
                name: "NumberLocation",
                table: "Apartments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StreetLocation",
                table: "Apartments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberLocation",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "StreetLocation",
                table: "Apartments");

            migrationBuilder.AddColumn<int>(
                name: "Number_location",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street_location",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

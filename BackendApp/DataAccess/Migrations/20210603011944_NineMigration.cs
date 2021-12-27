using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class NineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberLocation",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "StreetLocation",
                table: "Apartments");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Apartments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Apartments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Apartments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Apartments");

            migrationBuilder.AddColumn<int>(
                name: "NumberLocation",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StreetLocation",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Apartments_ApartmentId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_ApartmentId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Elements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Elements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ApartmentId",
                table: "Elements",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Apartments_ApartmentId",
                table: "Elements",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

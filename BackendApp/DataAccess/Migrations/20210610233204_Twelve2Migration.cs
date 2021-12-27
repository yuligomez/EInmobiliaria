using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Twelve2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementsApartment_Apartments_ApartmentId",
                table: "ElementsApartment");

            migrationBuilder.DropIndex(
                name: "IX_ElementsApartment_ApartmentId",
                table: "ElementsApartment");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "ElementsApartment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "ElementsApartment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ElementsApartment_ApartmentId",
                table: "ElementsApartment",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsApartment_Apartments_ApartmentId",
                table: "ElementsApartment",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

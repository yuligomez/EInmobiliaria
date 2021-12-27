using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Twelve3Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementsApartment_Users_UserId",
                table: "ElementsApartment");

            migrationBuilder.DropIndex(
                name: "IX_ElementsApartment_UserId",
                table: "ElementsApartment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ElementsApartment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ElementsApartment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ElementsApartment_UserId",
                table: "ElementsApartment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsApartment_Users_UserId",
                table: "ElementsApartment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

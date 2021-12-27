using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class TwelveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementsApartment_Elements_ElementId",
                table: "ElementsApartment");

            migrationBuilder.DropIndex(
                name: "IX_ElementsApartment_ElementId",
                table: "ElementsApartment");

            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "ElementsApartment");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Elements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Elements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Checks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Apartments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ApartmentId",
                table: "Elements",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_UserId",
                table: "Elements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Apartments_ApartmentId",
                table: "Elements",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Users_UserId",
                table: "Elements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Apartments_ApartmentId",
                table: "Elements");

            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Users_UserId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_ApartmentId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_UserId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Elements");

            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "ElementsApartment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Checks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Apartments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementsApartment_ElementId",
                table: "ElementsApartment",
                column: "ElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementsApartment_Elements_ElementId",
                table: "ElementsApartment",
                column: "ElementId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

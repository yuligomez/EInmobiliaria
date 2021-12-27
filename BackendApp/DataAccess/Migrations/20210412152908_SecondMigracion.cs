using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SecondMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Check_Apartment_ApartmentId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_Check_Users_UserId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_Apartment_ApartmentId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_Photo_PhotoId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Apartment_ApartmentId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Apartment_ApartmentId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Apartment_ApartmentId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Content",
                table: "Content");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Check",
                table: "Check");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameTable(
                name: "Content",
                newName: "Contents");

            migrationBuilder.RenameTable(
                name: "Check",
                newName: "Checks");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Apartments");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_ApartmentId",
                table: "Photos",
                newName: "IX_Photos_ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_PhotoId",
                table: "Contents",
                newName: "IX_Contents_PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_ApartmentId",
                table: "Contents",
                newName: "IX_Contents_ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Check_UserId",
                table: "Checks",
                newName: "IX_Checks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Check_ApartmentId",
                table: "Checks",
                newName: "IX_Checks_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checks",
                table: "Checks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Apartments_ApartmentId",
                table: "Checks",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Users_UserId",
                table: "Checks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Apartments_ApartmentId",
                table: "Contents",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Photos_PhotoId",
                table: "Contents",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Apartments_ApartmentId",
                table: "Photos",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Apartments_ApartmentId",
                table: "Registration",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Apartments_ApartmentId",
                table: "Rental",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Apartments_ApartmentId",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Users_UserId",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Apartments_ApartmentId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Photos_PhotoId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Apartments_ApartmentId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Apartments_ApartmentId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Apartments_ApartmentId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checks",
                table: "Checks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "Content");

            migrationBuilder.RenameTable(
                name: "Checks",
                newName: "Check");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Apartment");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_ApartmentId",
                table: "Photo",
                newName: "IX_Photo_ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_PhotoId",
                table: "Content",
                newName: "IX_Content_PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_ApartmentId",
                table: "Content",
                newName: "IX_Content_ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Checks_UserId",
                table: "Check",
                newName: "IX_Check_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Checks_ApartmentId",
                table: "Check",
                newName: "IX_Check_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Content",
                table: "Content",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Check",
                table: "Check",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Apartment_ApartmentId",
                table: "Check",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Users_UserId",
                table: "Check",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Apartment_ApartmentId",
                table: "Content",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Photo_PhotoId",
                table: "Content",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Apartment_ApartmentId",
                table: "Photo",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Apartment_ApartmentId",
                table: "Registration",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Apartment_ApartmentId",
                table: "Rental",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

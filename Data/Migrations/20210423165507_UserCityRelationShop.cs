using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.Data.Migrations
{
    public partial class UserCityRelationShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityModelID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityModelID",
                table: "AspNetUsers",
                column: "CityModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityModelID",
                table: "AspNetUsers",
                column: "CityModelID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityModelID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityModelID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityModelID",
                table: "AspNetUsers");
        }
    }
}

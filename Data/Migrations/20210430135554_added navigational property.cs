using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.Data.Migrations
{
    public partial class addednavigationalproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoryModel_ParentID",
                table: "CategoryModel",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryModel_CategoryModel_ParentID",
                table: "CategoryModel",
                column: "ParentID",
                principalTable: "CategoryModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryModel_CategoryModel_ParentID",
                table: "CategoryModel");

            migrationBuilder.DropIndex(
                name: "IX_CategoryModel_ParentID",
                table: "CategoryModel");
        }
    }
}

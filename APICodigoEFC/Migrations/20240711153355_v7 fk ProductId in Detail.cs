using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICodigoEFC.Migrations
{
    /// <inheritdoc />
    public partial class v7fkProductIdinDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Details_ProductID",
                table: "Details",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Products_ProductID",
                table: "Details",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Products_ProductID",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_ProductID",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Details");
        }
    }
}

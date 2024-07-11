using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICodigoEFC.Migrations
{
    /// <inheritdoc />
    public partial class v12deleteesActivoinProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsActivo",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsActivo",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

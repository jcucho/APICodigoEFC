using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICodigoEFC.Migrations
{
    /// <inheritdoc />
    public partial class v11modifyisActiveforesActivoinProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "Products",
                newName: "EsActivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EsActivo",
                table: "Products",
                newName: "Activo");
        }
    }
}

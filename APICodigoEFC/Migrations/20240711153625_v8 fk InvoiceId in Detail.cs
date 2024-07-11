using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICodigoEFC.Migrations
{
    /// <inheritdoc />
    public partial class v8fkInvoiceIdinDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceID",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Details_InvoiceID",
                table: "Details",
                column: "InvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Invoices_InvoiceID",
                table: "Details",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Invoices_InvoiceID",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_InvoiceID",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "InvoiceID",
                table: "Details");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class InvoiceModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InventoryCategory_InventoryCategoryId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_InventoryCategoryId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InventoryCategoryId",
                table: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InventoryCategoryId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InventoryCategoryId",
                table: "Invoice",
                column: "InventoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InventoryCategory_InventoryCategoryId",
                table: "Invoice",
                column: "InventoryCategoryId",
                principalTable: "InventoryCategory",
                principalColumn: "InventoryCategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

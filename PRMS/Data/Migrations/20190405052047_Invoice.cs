using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InventoryItem_InventoryItemId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_InventoryItemId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                table: "Invoice");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ItemId",
                table: "Invoice",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InventoryItem_ItemId",
                table: "Invoice",
                column: "ItemId",
                principalTable: "InventoryItem",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InventoryItem_ItemId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ItemId",
                table: "Invoice");

            migrationBuilder.AddColumn<long>(
                name: "InventoryItemId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InventoryItemId",
                table: "Invoice",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InventoryItem_InventoryItemId",
                table: "Invoice",
                column: "InventoryItemId",
                principalTable: "InventoryItem",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

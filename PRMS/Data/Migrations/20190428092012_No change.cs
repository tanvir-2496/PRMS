using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class Nochange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InventoryItem_ItemId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CollectionType",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InvoiceStatus",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "PaidMonthYear",
                table: "Invoice",
                newName: "InvoiceNo");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Invoice",
                newName: "InventoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_ItemId",
                table: "Invoice",
                newName: "IX_Invoice_InventoryItemId");

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserIp = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsRemove = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<long>(nullable: true),
                    InvoiceDetailsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<long>(nullable: true),
                    ItemId = table.Column<long>(nullable: true),
                    PaidMonthYear = table.Column<string>(maxLength: 30, nullable: true),
                    CollectionType = table.Column<string>(maxLength: 20, nullable: true),
                    PaidAmount = table.Column<decimal>(nullable: true),
                    PaymentStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetailsId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_InventoryItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ItemId",
                table: "InvoiceDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InventoryItem_InventoryItemId",
                table: "Invoice",
                column: "InventoryItemId",
                principalTable: "InventoryItem",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InventoryItem_InventoryItemId",
                table: "Invoice");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "InvoiceNo",
                table: "Invoice",
                newName: "PaidMonthYear");

            migrationBuilder.RenameColumn(
                name: "InventoryItemId",
                table: "Invoice",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_InventoryItemId",
                table: "Invoice",
                newName: "IX_Invoice_ItemId");

            migrationBuilder.AddColumn<string>(
                name: "CollectionType",
                table: "Invoice",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceStatus",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InventoryItem_ItemId",
                table: "Invoice",
                column: "ItemId",
                principalTable: "InventoryItem",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class InvoiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionMonth",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "CollectionYear",
                table: "Invoice",
                newName: "PaidMonth");

            migrationBuilder.RenameColumn(
                name: "CollectionDate",
                table: "Invoice",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Invoice",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Invoice",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "DueAmount",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InventoryCategoryId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemove",
                table: "Invoice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaidYear",
                table: "Invoice",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Invoice",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIp",
                table: "Invoice",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentUrl",
                table: "CustomerDocument",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InventoryCategory_InventoryCategoryId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_InventoryCategoryId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DueAmount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InventoryCategoryId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IsRemove",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PaidYear",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UserIp",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Invoice",
                newName: "CollectionDate");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Invoice",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "PaidMonth",
                table: "Invoice",
                newName: "CollectionYear");

            migrationBuilder.AddColumn<string>(
                name: "CollectionMonth",
                table: "Invoice",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentUrl",
                table: "CustomerDocument",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}

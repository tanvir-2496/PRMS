using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class ExpanceModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expance_InventoryItem_InventoryItemId",
                table: "Expance");

            migrationBuilder.DropIndex(
                name: "IX_Expance_InventoryItemId",
                table: "Expance");

            migrationBuilder.AlterColumn<string>(
                name: "ExpanceType",
                table: "Expance",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Expance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonthYear",
                table: "Expance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Expance");

            migrationBuilder.DropColumn(
                name: "MonthYear",
                table: "Expance");

            migrationBuilder.AlterColumn<string>(
                name: "ExpanceType",
                table: "Expance",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.CreateIndex(
                name: "IX_Expance_InventoryItemId",
                table: "Expance",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expance_InventoryItem_InventoryItemId",
                table: "Expance",
                column: "InventoryItemId",
                principalTable: "InventoryItem",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class Inv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidMonth",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PaidYear",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "PaidMonthYear",
                table: "Invoice",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidMonthYear",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "PaidMonth",
                table: "Invoice",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaidYear",
                table: "Invoice",
                maxLength: 10,
                nullable: true);
        }
    }
}

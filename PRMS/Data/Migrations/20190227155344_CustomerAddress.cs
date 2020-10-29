using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class CustomerAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.AddColumn<string>(
                name: "PermanentAddress",
                table: "CustomerInfo",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentAddress",
                table: "CustomerInfo",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermanentAddress",
                table: "CustomerInfo");

            migrationBuilder.DropColumn(
                name: "PresentAddress",
                table: "CustomerInfo");

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    CustomerAddressId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    PermanentAddress = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddress = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserIp = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.CustomerAddressId);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_CustomerInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId");
        }
    }
}

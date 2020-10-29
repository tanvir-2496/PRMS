using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRMS.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
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
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    FatherName = table.Column<string>(maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(maxLength: 50, nullable: false),
                    SpouseName = table.Column<string>(maxLength: 50, nullable: true),
                    NID = table.Column<string>(maxLength: 30, nullable: true),
                    BirthId = table.Column<string>(maxLength: 30, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Contact1 = table.Column<string>(maxLength: 20, nullable: true),
                    Contact2 = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerImage = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryCategory",
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
                    InventoryCategoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryCategory", x => x.InventoryCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
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
                    CustomerAddressId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PresentAddress = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentAddress = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerId = table.Column<long>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "CustomerDocument",
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
                    CustomerDocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentUrl = table.Column<string>(maxLength: 250, nullable: false),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    ReferanceId = table.Column<int>(nullable: true),
                    Referance = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocument", x => x.CustomerDocumentId);
                    table.ForeignKey(
                        name: "FK_CustomerDocument_CustomerInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
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
                    InventoryItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    ItemCode = table.Column<string>(maxLength: 50, nullable: true),
                    Location = table.Column<string>(maxLength: 250, nullable: true),
                    InventoryCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.InventoryItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItem_InventoryCategory_InventoryCategoryId",
                        column: x => x.InventoryCategoryId,
                        principalTable: "InventoryCategory",
                        principalColumn: "InventoryCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aggrement",
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
                    AggrementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AggrementDate = table.Column<DateTime>(nullable: true),
                    AggrementStartDate = table.Column<DateTime>(nullable: true),
                    AggrementEndDate = table.Column<DateTime>(nullable: true),
                    AggrementAmount = table.Column<decimal>(nullable: true),
                    MonthlyRent = table.Column<decimal>(nullable: true),
                    InventoryItemId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggrement", x => x.AggrementId);
                    table.ForeignKey(
                        name: "FK_Aggrement_CustomerInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aggrement_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expance",
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
                    ExpanceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExpanceType = table.Column<string>(maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    ExpanceDate = table.Column<DateTime>(nullable: false),
                    InventoryItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expance", x => x.ExpanceId);
                    table.ForeignKey(
                        name: "FK_Expance_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionDate = table.Column<DateTime>(nullable: false),
                    CollectionMonth = table.Column<string>(maxLength: 20, nullable: true),
                    CollectionYear = table.Column<string>(maxLength: 20, nullable: true),
                    CollectionType = table.Column<string>(maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    InvoiceStatus = table.Column<int>(nullable: true),
                    ItemId = table.Column<long>(nullable: true),
                    InventoryItemId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_CustomerInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aggrement_CustomerId",
                table: "Aggrement",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Aggrement_InventoryItemId",
                table: "Aggrement",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocument_CustomerId",
                table: "CustomerDocument",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Expance_InventoryItemId",
                table: "Expance",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_InventoryCategoryId",
                table: "InventoryItem",
                column: "InventoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InventoryItemId",
                table: "Invoice",
                column: "InventoryItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aggrement");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "CustomerDocument");

            migrationBuilder.DropTable(
                name: "Expance");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "InventoryCategory");
        }
    }
}

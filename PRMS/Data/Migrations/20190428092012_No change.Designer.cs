﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PRMS.Data;

namespace PRMS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190428092012_No change")]
    partial class Nochange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PRMS.Models.Aggrement", b =>
                {
                    b.Property<long>("AggrementId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AggrementAmount");

                    b.Property<DateTime?>("AggrementDate");

                    b.Property<DateTime?>("AggrementEndDate");

                    b.Property<DateTime?>("AggrementStartDate");

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("CustomerId");

                    b.Property<long?>("InventoryItemId");

                    b.Property<bool>("IsRemove");

                    b.Property<decimal?>("MonthlyRent");

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("AggrementId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("Aggrement");
                });

            modelBuilder.Entity("PRMS.Models.Company", b =>
                {
                    b.Property<long>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(250);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("PRMS.Models.CustomerDocument", b =>
                {
                    b.Property<long>("CustomerDocumentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CustomerId");

                    b.Property<string>("DocumentUrl")
                        .HasMaxLength(250);

                    b.Property<bool>("IsRemove");

                    b.Property<string>("Referance")
                        .HasMaxLength(250);

                    b.Property<int?>("ReferanceId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(250);

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("CustomerDocumentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerDocument");
                });

            modelBuilder.Entity("PRMS.Models.CustomerInfo", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BirthId")
                        .HasMaxLength(30);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("Contact1")
                        .HasMaxLength(20);

                    b.Property<string>("Contact2")
                        .HasMaxLength(20);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerImage")
                        .HasMaxLength(250);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<bool>("IsRemove");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MotherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NID")
                        .HasMaxLength(30);

                    b.Property<string>("PermanentAddress")
                        .HasMaxLength(250);

                    b.Property<string>("PresentAddress")
                        .HasMaxLength(250);

                    b.Property<string>("SpouseName")
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("CustomerId");

                    b.ToTable("CustomerInfo");
                });

            modelBuilder.Entity("PRMS.Models.Expance", b =>
                {
                    b.Property<long>("ExpanceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("CustomerId");

                    b.Property<DateTime>("ExpanceDate");

                    b.Property<string>("ExpanceType")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<long?>("InventoryItemId");

                    b.Property<bool>("IsRemove");

                    b.Property<string>("MonthYear");

                    b.Property<string>("Remarks")
                        .HasMaxLength(250);

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("ExpanceId");

                    b.ToTable("Expance");
                });

            modelBuilder.Entity("PRMS.Models.InventoryCategory", b =>
                {
                    b.Property<long>("InventoryCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100);

                    b.Property<bool>("IsRemove");

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("InventoryCategoryId");

                    b.ToTable("InventoryCategory");
                });

            modelBuilder.Entity("PRMS.Models.InventoryItem", b =>
                {
                    b.Property<long>("InventoryItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("InventoryCategoryId");

                    b.Property<bool>("IsRemove");

                    b.Property<string>("ItemCode")
                        .HasMaxLength(50);

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Location")
                        .HasMaxLength(250);

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("InventoryItemId");

                    b.HasIndex("InventoryCategoryId");

                    b.ToTable("InventoryItem");
                });

            modelBuilder.Entity("PRMS.Models.Invoice", b =>
                {
                    b.Property<long>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("CustomerId");

                    b.Property<long?>("InventoryItemId");

                    b.Property<DateTime>("InvoiceDate");

                    b.Property<string>("InvoiceNo")
                        .HasMaxLength(30);

                    b.Property<bool>("IsRemove");

                    b.Property<int>("Status");

                    b.Property<decimal?>("TotalAmount");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("PRMS.Models.InvoiceDetails", b =>
                {
                    b.Property<long>("InvoiceDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CollectionType")
                        .HasMaxLength(20);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("InvoiceId");

                    b.Property<bool>("IsRemove");

                    b.Property<long?>("ItemId");

                    b.Property<decimal?>("PaidAmount");

                    b.Property<string>("PaidMonthYear")
                        .HasMaxLength(30);

                    b.Property<int?>("PaymentStatus");

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserIp")
                        .HasMaxLength(100);

                    b.HasKey("InvoiceDetailsId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ItemId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PRMS.Models.Aggrement", b =>
                {
                    b.HasOne("PRMS.Models.CustomerInfo", "CustomerInfo")
                        .WithMany("Aggrement")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PRMS.Models.InventoryItem", "InventoryItem")
                        .WithMany("Aggrement")
                        .HasForeignKey("InventoryItemId");
                });

            modelBuilder.Entity("PRMS.Models.CustomerDocument", b =>
                {
                    b.HasOne("PRMS.Models.CustomerInfo", "CustomerInfo")
                        .WithMany("CustomerDocument")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PRMS.Models.InventoryItem", b =>
                {
                    b.HasOne("PRMS.Models.InventoryCategory", "InventoryCategory")
                        .WithMany("InventoryItem")
                        .HasForeignKey("InventoryCategoryId");
                });

            modelBuilder.Entity("PRMS.Models.Invoice", b =>
                {
                    b.HasOne("PRMS.Models.CustomerInfo", "CustomerInfo")
                        .WithMany("Invoice")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PRMS.Models.InventoryItem")
                        .WithMany("Invoice")
                        .HasForeignKey("InventoryItemId");
                });

            modelBuilder.Entity("PRMS.Models.InvoiceDetails", b =>
                {
                    b.HasOne("PRMS.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("PRMS.Models.InventoryItem", "InventoryItem")
                        .WithMany()
                        .HasForeignKey("ItemId");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRMS.Models;

namespace PRMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Aggrement> Aggrement { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CustomerDocument> CustomerDocument { get; set; }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<Expance> Expance { get; set; }
        public DbSet<InventoryCategory> InventoryCategory { get; set; }
        public DbSet<InventoryItem> InventoryItem { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
    }
}

using CompanyOrderManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyOrderManagementService.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders {  get; set; }
        
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasAlternateKey(c => c.CompanyName);
            modelBuilder.Entity<Products>().HasAlternateKey(p => p.ProductName);
            
            //Relationships
             
             // Many to One  Products - Company
             modelBuilder.Entity<Products>()
                 .HasOne(p => p.Company)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CompanyName)
                 .HasPrincipalKey(c => c.CompanyName)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            // Many to One  Order - Company
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CompanyName)
                .HasPrincipalKey(c => c.CompanyName)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Many to One  Order - Products
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Products)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ProductName)
                .HasPrincipalKey(p => p.ProductName)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}

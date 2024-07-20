using CompanyOrderManagementService.Data;
using CompanyOrderManagementService.Models;
using Microsoft.AspNetCore.Identity;

namespace CompanyOrderManagementService
{
    public class Seed
    {

        private readonly ApplicationDBContext _context;

        public Seed(ApplicationDBContext context)
        {
            _context = context;
 
        }

        public void SeedDbContext()
        {
            if (!_context.Companies.Any())
            {
                var companies = new List<Company>()
                {
                    new Company { CompanyName = "Enoca A", ApprovalStatus = true, OrderPermissionStartTime = TimeSpan.FromHours(8), OrderPermissionEndTime = TimeSpan.FromHours(18) },
                    new Company { CompanyName = "Enoca B", ApprovalStatus = false, OrderPermissionStartTime = TimeSpan.FromHours(10), OrderPermissionEndTime = TimeSpan.FromHours(21) },
                    new Company { CompanyName = "Enoca C", ApprovalStatus = true, OrderPermissionStartTime = TimeSpan.FromHours(9), OrderPermissionEndTime = TimeSpan.FromHours(11) },
                    new Company { CompanyName = "Enoca D", ApprovalStatus = true, OrderPermissionStartTime = TimeSpan.FromHours(9), OrderPermissionEndTime = TimeSpan.FromHours(15) }
                    
                };

                _context.Companies.AddRange(companies);
            }

            if (!_context.Products.Any())
            {
                var products = new List<Products>()
                {
                    new Products { ProductName = "Product 1", Stock = 100, Price = 10.5m, CompanyName = "Enoca A" },
                    new Products { ProductName = "Product 2", Stock = 50, Price = 20.75m, CompanyName = "Enoca B"  },
                    new Products { ProductName = "Product 3", Stock = 200, Price = 5.99m, CompanyName = "Enoca C" },
                    new Products { ProductName = "Product 4", Stock = 2000, Price = 7.5m, CompanyName = "Enoca D" }
                };

                _context.Products.AddRange(products);
            }

            if (!_context.Orders.Any())
            {
                var orders = new List<Order>()
                {
                    new Order { CompanyName = "Enoca A" , ProductName = "Product 1", PersonName = "Baha YOLAL", OrderDate = DateTime.UtcNow },
                    new Order { CompanyName = "Enoca B" , ProductName = "Product 2", PersonName = "Eren KAR", OrderDate = DateTime.UtcNow },
                    new Order { CompanyName = "Enoca C" , ProductName = "Product 3", PersonName = "Ali KIRLANGIC", OrderDate = DateTime.UtcNow },
                    new Order { CompanyName = "Enoca D" , ProductName = "Product 4", PersonName = "Gülbahar Seckin", OrderDate = DateTime.UtcNow }
                };

                _context.Orders.AddRange(orders);
            }

            _context.SaveChanges();
        }
    }
}

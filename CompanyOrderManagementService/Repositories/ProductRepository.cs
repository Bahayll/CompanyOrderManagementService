using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyOrderManagementService.Data;
using CompanyOrderManagementService.Interfaces;
using CompanyOrderManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyOrderManagementService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Products?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Products?> GetProductByNameAsync(string productName)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductName == productName);
        }
        public async Task<IEnumerable<Products?>> GetProductsByCompanyNameAsync(string companyName)
        {
            return await _context.Products.Where(p => p.CompanyName == companyName).ToListAsync();
        }
        public async Task<Products?> CheckIfCompanySellsProduct(string productName, string companyName)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductName == productName && x.CompanyName == companyName);
        }

        public async Task<Products?> AddProductAsync(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Products?> UpdateProductAsync(int id, Products updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Stock = updatedProduct.Stock;
            product.Price = updatedProduct.Price;
            product.CompanyName = updatedProduct.CompanyName;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

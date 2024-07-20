using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyOrderManagementService.Models;

namespace CompanyOrderManagementService.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetProductsAsync();
        Task<Products?> GetProductByIdAsync(int id);
        Task<Products?> GetProductByNameAsync(string productName);
        Task<Products?> CheckIfCompanySellsProduct(string productName, string companyName);
        Task<IEnumerable<Products?>> GetProductsByCompanyNameAsync(string companyName);
        Task<Products?> AddProductAsync(Products product);
        Task<Products?> UpdateProductAsync(int id, Products updatedProduct);
        Task<bool> DeleteProductAsync(int id);
    }
}

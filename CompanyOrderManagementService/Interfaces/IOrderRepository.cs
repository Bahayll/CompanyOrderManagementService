using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyOrderManagementService.Models;

namespace CompanyOrderManagementService.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> AddOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(int id, Order updatedOrder);
        Task<bool> DeleteOrderAsync(int id);
        Task<Order?> GetOrderByDetailsAsync(string personName, DateTime orderDate, string companyName, string productName); // To check if the order has already been placed 
    }
}

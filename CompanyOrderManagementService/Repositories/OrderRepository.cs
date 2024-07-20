using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyOrderManagementService.Data;
using CompanyOrderManagementService.Interfaces;
using CompanyOrderManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyOrderManagementService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order updatedOrder)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            order.PersonName = updatedOrder.PersonName;
            order.OrderDate = updatedOrder.OrderDate;
            order.CompanyName = updatedOrder.CompanyName;
            order.ProductName = updatedOrder.ProductName;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        // To check if the order has already been placed
        public async Task<Order?> GetOrderByDetailsAsync(string personName, DateTime orderDate, string companyName, string productName)
        {
            return await _context.Orders.FirstOrDefaultAsync(
                o => o.PersonName == personName
                    && o.OrderDate == orderDate
                    && o.Company.CompanyName == companyName
                    && o.Products.ProductName == productName);
        }
    }
}

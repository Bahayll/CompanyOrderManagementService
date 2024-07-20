using CompanyOrderManagementService.DTO.CompanyPostDTO;
using CompanyOrderManagementService.DTO.OrderDTO;
using CompanyOrderManagementService.Interfaces;
using CompanyOrderManagementService.Mappers;
using CompanyOrderManagementService.Models;
using CompanyOrderManagementService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyOrderManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, ICompanyRepository companyRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _companyRepository = companyRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// List Order
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found.");
            }
            return Ok(orders.toOrdersDTO());
        }

        /// <summary>
        /// List Order with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order.toOrderDTO());
        }

        /// <summary>
        /// Add Order (Company controls the order permission hours.)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(OrderDTO))] // Created
        [ProducesResponseType(400)] // Bad Request 
        [ProducesResponseType(404)] // Not Found 
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<IActionResult> AddOrder([FromBody] OrderPostDTO orderPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (orderPostDTO.CompanyName == null || orderPostDTO.ProductName == null)
            {
                return BadRequest("Company name and product name cannot be null.");
            }

            // Get company information
            var company = await _companyRepository.GetCompanyByNameAsync(orderPostDTO.CompanyName);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            // Company approval status check
            if (!company.ApprovalStatus)
            {
                return BadRequest("Company is not approved.");
            }

            // Let's check whether the company concerned is selling the product.
            var product = await _productRepository.CheckIfCompanySellsProduct(orderPostDTO.ProductName, orderPostDTO.CompanyName);
            if (product == null)
            {
                return NotFound("Company does not sell this product or Product not found.");
            }

            // Turkey's local time is in the UTC+3 time zone. 
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey");
            DateTime turkeyTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, turkeyTimeZone);
            TimeSpan turkeyTimeOfDay = turkeyTime.TimeOfDay;

            // Current time to order
            var currentTime = turkeyTimeOfDay; 

            // Order permission hours
            var startTime = company.OrderPermissionStartTime;
            var endTime = company.OrderPermissionEndTime;

            // Checking the order time
            if (currentTime < startTime || currentTime > endTime)
            {
                return BadRequest("Company is not accepting orders at this time.");
            }
            // To check if the order has already been placed
            var existingOrder = await _orderRepository.GetOrderByDetailsAsync(orderPostDTO.PersonName, orderPostDTO.OrderDate, orderPostDTO.CompanyName, orderPostDTO.ProductName);
            if (existingOrder != null)
            {
                return Conflict("This order has already been placed.");
            }

            // Adding an order
            var order = orderPostDTO.toOrder();
            order.OrderDate = turkeyTime;
            var createdOrder = await _orderRepository.AddOrderAsync(order);

            if (createdOrder == null)
            {
                return StatusCode(500, "Failed to add order.");
            }

            return Ok(createdOrder.toOrderDTO());
        }




        /// <summary>
        /// Update Order (Company controls the order permission hours.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderPostDTO orderPostDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (orderPostDTO == null)
            {
                return BadRequest("Order data is null.");
            }

            // Get company information
            var company = await _companyRepository.GetCompanyByNameAsync(orderPostDTO.CompanyName);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            // Let's check whether the company concerned is selling the product.
            var product = await _productRepository.CheckIfCompanySellsProduct(orderPostDTO.ProductName, orderPostDTO.CompanyName);
            if (product == null)
            {
                return NotFound("Company does not sell this product or Product not found.");
            }

            // Order time to be updated
            TimeSpan orderTime = orderPostDTO.OrderDate.TimeOfDay;


            // Order permission hours
            var startTime = company.OrderPermissionStartTime;
            var endTime = company.OrderPermissionEndTime;

            // Cannot sell if the product does not belong to the company
            // Checking the order time
            if (orderTime< startTime || orderTime > endTime)
            {
                return BadRequest("Company is not accepting orders at this time.");
            }

            // To check if the order has already been placed
            var existingOrder = await _orderRepository.GetOrderByDetailsAsync(orderPostDTO.PersonName, orderPostDTO.OrderDate, orderPostDTO.CompanyName, orderPostDTO.ProductName);
            if (existingOrder != null)
            {
                return Conflict("This order has already been placed.");
            }

            var updatedOrder = orderPostDTO.toOrder();

            var orderDto = await _orderRepository.UpdateOrderAsync(id, updatedOrder);

            if (orderDto == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(orderDto.toOrderDTO());
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var success = await _orderRepository.DeleteOrderAsync(id);
            if (!success)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return NoContent();
        }
    }
}

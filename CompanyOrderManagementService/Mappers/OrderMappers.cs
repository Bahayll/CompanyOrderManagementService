using CompanyOrderManagementService.DTO.CompanyDTO;
using CompanyOrderManagementService.DTO.OrderDTO;
using CompanyOrderManagementService.Models;

namespace CompanyOrderManagementService.Mappers
{
    public static class OrderMappers
    {
        public static Order toOrder(this OrderPostDTO orderPostDTO)
        {
            return new Order()
            {
                PersonName = orderPostDTO.PersonName,
                OrderDate = orderPostDTO.OrderDate,
                CompanyName = orderPostDTO.CompanyName,
                ProductName = orderPostDTO.ProductName
            };
        }
        public static OrderDTO toOrderDTO(this Order order)
        {
            return new OrderDTO()
            {
                OrderId = order.OrderId,
                PersonName = order.PersonName,
                OrderDate = order.OrderDate,
                CompanyName = order.CompanyName,
                ProductName = order.ProductName
            };
        }
        public static List<OrderDTO> toOrdersDTO(this IEnumerable<Order> orders)
        {
            var returnList = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var dto = new OrderDTO()
                {
                    OrderId = order.OrderId,
                    PersonName = order.PersonName,
                    OrderDate = order.OrderDate,
                    CompanyName = order.CompanyName,
                    ProductName = order.ProductName
                };
                returnList.Add(dto);
            }
            return returnList;
        }
    }
}

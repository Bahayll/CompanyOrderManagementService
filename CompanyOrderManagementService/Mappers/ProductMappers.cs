using CompanyOrderManagementService.DTO.ProductDTO;
using CompanyOrderManagementService.Models;

namespace CompanyOrderManagementService.Mappers
{
    public static class ProductMappers
    {

        public static Products toProductUpdateDTO(this ProductUpdateDTO product)
        {
            return new Products
            {
                Stock = product.Stock,
                Price = product.Price,
                CompanyName = product.CompanyName
            };
        }
        public static Products toProductPostDTO(this ProductPostDTO product)
        {
            return new Products
            {
                ProductName = product.ProductName,
                Stock = product.Stock,
                Price = product.Price,
                CompanyName = product.CompanyName
            };
        }
        public static ProductDTO toProductDTO(this Products products)
        {
            return new ProductDTO
            {
                ProductId = products.ProductId,
                ProductName = products.ProductName,
                Stock = products.Stock,
                Price = products.Price,
                CompanyName = products.CompanyName
            };
        }

        public static List<ProductDTO> toProductsDTO(this IEnumerable<Products> products)
        {
            var returnList = new List<ProductDTO>();
            foreach (var product in products)
            {
                var dto = new ProductDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Stock = product.Stock,
                    Price = product.Price,
                    CompanyName = product.CompanyName
                };
                returnList.Add(dto);
            }
            return returnList;
        }


    }
}

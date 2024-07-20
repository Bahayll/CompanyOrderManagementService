using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.DTO.ProductDTO
{
    public class ProductUpdateDTO
    {
        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? CompanyName { get; set; }
    }
}

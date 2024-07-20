using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.DTO.ProductDTO
{
    public class ProductPostDTO
    {
        [Required]
        public string? ProductName { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? CompanyName { get; set; }
    }
}

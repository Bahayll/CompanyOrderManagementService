using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.DTO.ProductDTO
{
    public class ProductDTO
    {


        [Required]
        public int ProductId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string? CompanyName { get; set; }
    }
}

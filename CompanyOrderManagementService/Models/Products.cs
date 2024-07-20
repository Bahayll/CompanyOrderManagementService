using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.Models
{
    public class Products
    {
        

        // Scaler Property

        [Key]
        public int ProductId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        
        public string? CompanyName { get; set; }


        // Navigation property
        public Company? Company { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }   
}


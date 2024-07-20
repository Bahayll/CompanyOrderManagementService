
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.Models
{
    public class Order
    {
        //Scaler Property

        [Key]
        public int OrderId { get; set; }

        [Required]
        public string? PersonName { get; set; } // Name of the ordering person

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? ProductName { get; set; }

        // Navigation Property

        public Company? Company { get; set; }
        public Products? Products { get; set; }
    }
}

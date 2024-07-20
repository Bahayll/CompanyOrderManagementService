using System.ComponentModel.DataAnnotations;



namespace CompanyOrderManagementService.DTO.OrderDTO
{
    public class OrderPostDTO
    {
        [Required]
        public string? PersonName { get; set; } // Name of the ordering person

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? ProductName { get; set; }
    }
}


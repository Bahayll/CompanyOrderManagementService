using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.Models
{
    public class Company
    {
        // Scaler Property
        [Key]
        public int CompanyId { get; set; } // Primary Key

        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public bool ApprovalStatus { get; set; }

        [Required]
        public TimeSpan OrderPermissionStartTime { get; set; }

        [Required]
        public TimeSpan OrderPermissionEndTime { get; set; }

        // Navigation property
        public ICollection<Products>? Products { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}

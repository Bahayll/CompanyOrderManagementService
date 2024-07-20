using System.ComponentModel.DataAnnotations;


namespace CompanyOrderManagementService.DTO.CompanyDTO
{
    public class CompanyDTO
    {

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public bool ApprovalStatus { get; set; }

        [Required]
        public TimeSpan OrderPermissionStartTime { get; set; }

        [Required]
        public TimeSpan OrderPermissionEndTime { get; set; }
    }
}

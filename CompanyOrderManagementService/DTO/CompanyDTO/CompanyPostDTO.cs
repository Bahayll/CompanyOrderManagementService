using System.ComponentModel.DataAnnotations;


namespace CompanyOrderManagementService.DTO.CompanyPostDTO
{
    public class CompanyPostDTO
    {
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

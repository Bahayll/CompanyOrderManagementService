using System.ComponentModel.DataAnnotations;

namespace CompanyOrderManagementService.DTO.CompanyDTO
{
    public class CompanyUpdateDTO
    {

       
        [Required]
        public bool ApprovalStatus { get; set; }
        [Required]
        public TimeSpan OrderPermissionStartTime { get; set; }
        [Required]
        public TimeSpan OrderPermissionEndTime { get; set; }

    }
}

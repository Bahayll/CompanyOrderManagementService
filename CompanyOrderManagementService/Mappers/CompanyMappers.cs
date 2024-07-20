using CompanyOrderManagementService.DTO.CompanyDTO;
using CompanyOrderManagementService.DTO.CompanyPostDTO;
using CompanyOrderManagementService.Models;

namespace CompanyOrderManagementService.Mappers
{
    public static class CompanyMappers
    {

        public static Company toCompany(this CompanyPostDTO companyPostDto)
        {
            return new Company()
            {
                CompanyName = companyPostDto.CompanyName,
                ApprovalStatus = companyPostDto.ApprovalStatus,
                OrderPermissionStartTime = companyPostDto.OrderPermissionStartTime,
                OrderPermissionEndTime = companyPostDto.OrderPermissionEndTime
            };
        }

        public static CompanyDTO toCompanyDTO(this Company company)
        {
            return new CompanyDTO()
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                ApprovalStatus = company.ApprovalStatus,
                OrderPermissionStartTime = company.OrderPermissionStartTime,
                OrderPermissionEndTime = company.OrderPermissionEndTime
            };
        }
        public static List<CompanyDTO> toCompaniesDTO(this IEnumerable<Company> companies)
        {
            var returnList = new List<CompanyDTO>();

            foreach (var company in companies)
            {
                var dto = new CompanyDTO()
                {
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    ApprovalStatus = company.ApprovalStatus,
                    OrderPermissionStartTime = company.OrderPermissionStartTime,
                    OrderPermissionEndTime = company.OrderPermissionEndTime
                };
                returnList.Add(dto);
            }

            return returnList;
        }

        public static void UpdateCompanyFromDto(this Company company, CompanyUpdateDTO companyUpdateDTO)
        {
            company.ApprovalStatus = companyUpdateDTO.ApprovalStatus;
            company.OrderPermissionStartTime = companyUpdateDTO.OrderPermissionStartTime;
            company.OrderPermissionEndTime = companyUpdateDTO.OrderPermissionEndTime;
        }
    }
}

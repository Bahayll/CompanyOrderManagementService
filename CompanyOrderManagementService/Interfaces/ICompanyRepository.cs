using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyOrderManagementService.Models;

namespace CompanyOrderManagementService.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company?> GetCompanyByNameAsync(string name);
        Task<Company?> AddCompanyAsync(Company company);
        Task<bool?> UpdateCompanyAsync(Company updatedCompany);
        Task<bool> DeleteCompanyAsync(int id);
    }
}

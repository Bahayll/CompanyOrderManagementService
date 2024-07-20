using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyOrderManagementService.Data;
using CompanyOrderManagementService.Interfaces;
using CompanyOrderManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyOrderManagementService.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDBContext _context;

        public CompanyRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }


        public async Task<Company?>GetCompanyByNameAsync(string companyName) 
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyName == companyName);
            return company;
        }

      
        public async Task<Company?> AddCompanyAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool?> UpdateCompanyAsync(Company updatedCompany)
        {
            _context.Companies.Update(updatedCompany);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

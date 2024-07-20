using CompanyOrderManagementService.DTO.CompanyDTO;
using CompanyOrderManagementService.DTO.CompanyPostDTO;
using CompanyOrderManagementService.DTO.ProductDTO;
using CompanyOrderManagementService.Interfaces;
using CompanyOrderManagementService.Mappers;
using CompanyOrderManagementService.Models;
using CompanyOrderManagementService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyOrderManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }


        /// <summary>
        /// List Company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            if (companies == null || !companies.Any())
            {
                return NotFound("No companies found.");
            }
            return Ok(companies.toCompaniesDTO());
        }

        /// <summary>
        /// List company with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }
            return Ok(company.toCompanyDTO());
        }

        /// <summary>
        /// Add Company 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCompany([FromBody] CompanyPostDTO company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             // Check if a company with the same name already exists
            var existingCompany = await _companyRepository.GetCompanyByNameAsync(company.CompanyName);

            if (existingCompany != null)
            {
                return BadRequest("The company with this name already exists.");
            }


            var createdCompany = await _companyRepository.AddCompanyAsync(company.toCompany());
            if (createdCompany == null)
            {
                return StatusCode(500, "Failed to add company.");
            }
            return Ok(createdCompany.toCompanyDTO());
        }


        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyUpdateDTO companyUpdateDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _companyRepository.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            // Let's check if the current company is the same as the updated company
            if (company.ApprovalStatus == companyUpdateDto.ApprovalStatus &&
                company.OrderPermissionStartTime == companyUpdateDto.OrderPermissionStartTime &&
                company.OrderPermissionEndTime == companyUpdateDto.OrderPermissionEndTime)
            {
                return Ok("Company already up to date.");
            }

            company.UpdateCompanyFromDto(companyUpdateDto);
            
            var result = await _companyRepository.UpdateCompanyAsync(company);
            if (!result.HasValue || !result.Value)
            {
                return StatusCode(500, "There was an error in the company update.");
            }

              return Ok(company.toCompanyDTO()); // For Control Purposes
            // return NoContent(); // For Bandwidth Saving
        }


        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
             var success = await _companyRepository.DeleteCompanyAsync(id);
              if (!success)
              {
                    return NotFound($"Company with ID {id} not found.");
              }

              return NoContent();

        }

    }
}


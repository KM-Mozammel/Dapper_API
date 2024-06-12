using API.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        /*
         * Getting data from Interface->Implemented Class
         * And Getting by Constructor */

        private readonly ICompanyRepository _companyRepo;
        public CompaniesController(ICompanyRepository companyRepo) => _companyRepo = companyRepo;

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepo.GetCompanies();

            return Ok(companies);
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompanie(int id)
        {
            var company = await _companyRepo.GetCompany(id);

            if (company is null)
                return NotFound();
            return Ok(company);
        }

    }
}

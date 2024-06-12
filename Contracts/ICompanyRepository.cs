using API.Dto;
using API.Entities;

namespace API.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        //public Task CreateCompany(CompanyForCreationDTO company);

    }
}

using API.Context;
using API.Contracts;
using API.Dto;
using API.Entities;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Repository
{
    public class CompanyRepository : ICompanyRepository 
    {
        /*Catching Dapper context Class as _context by Constructor*/
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT Id, Name, Address, Country FROM Companies";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return company;
            }
        }

        //public async Task CreateCompany(CompanyForCreationDTO company)
        //{
        //    var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";
            
        //    var parameters = new DynamicParameters();

        //    parameters.Add("Name", company.Name, System.Data.DbType.String);
        //    parameters.Add("Address", company.Address, System.Data.DbType.String);
        //    parameters.Add("Country", company.Country, System.Data.DbType.String);

        //    using (var connection = _context.CreateConnection())
        //    {
        //        await connection.ExecuteAsync(query, parameters);
        //    }
        //}
    }
}
 
using API.Context;
using API.Contracts;
using API.Dto;
using API.Entities;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Reflection.Metadata;

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

        public async Task<Company> CreateCompany(CompanyForCreationDTO company)
        {
           var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" + "SELECT CAST(SCOPE_IDENTITY() AS int)";
            
           var parameters = new DynamicParameters();

           parameters.Add("Name", company.Name, DbType.String);
           parameters.Add("Address", company.Address, DbType.String);
           parameters.Add("Country", company.Country, DbType.String);

           using (var connection = _context.CreateConnection())
           {
               var id = await connection.QuerySingleAsync<int>(query, parameters);
               var createdCompany = new Company
               {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country,
               };

               return createdCompany;
           }
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "UPDATE Companies SET Name = @name, Address = @address, Country = @country WHERE Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Companies WHERE Id = @id";
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
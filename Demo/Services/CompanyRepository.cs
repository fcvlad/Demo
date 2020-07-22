using Demo.Data;
using Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DemoDbContext _context;

        public CompanyRepository(DemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

       

        public void AddCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CompanyExistsAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}

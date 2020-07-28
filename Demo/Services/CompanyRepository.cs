using Demo.Data;
using Demo.DtoParameters;
using Demo.Entities;
using Demo.Helps;
using Microsoft.EntityFrameworkCore;
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
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.Id = Guid.NewGuid();
            if (company.Employees != null)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }
        
            _context.Companies.Add(company);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.CompanyId = companyId;
            _context.employees.Add(employee);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public void DeleteCompany(Company company)
        {
            _context.Companies.Remove(company);
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.employees.Remove(employee);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies.Where(x => companyIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<PagedList<IEnumerable<Company>>> GetCompaniesAsync(CompanyDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExpression = _context.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                parameters.CompanyName = parameters.CompanyName.Trim();
                queryExpression = queryExpression.Where(x => x.Name == parameters.CompanyName);
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                queryExpression = queryExpression.Where(x => x.Name.Contains(parameters.SearchTerm) || x.Introduction.Contains(parameters.SearchTerm));
            }
            return await PagedList<Company>.Create(queryExpression, parameters, parameters);
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _context.employees.Where(x => x.CompanyId == companyId && x.Id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay, string q)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (string.IsNullOrWhiteSpace(genderDisplay) && string.IsNullOrWhiteSpace(q))
            {
                return await _context.employees.Where(x => x.CompanyId == companyId).OrderBy(x => x.EmployeeNo).ToListAsync();
            }
            var items = _context.employees.Where(x => x.CompanyId == companyId);
            if (!string.IsNullOrWhiteSpace(genderDisplay))
            {
                genderDisplay = genderDisplay.Trim();
                var gender = Enum.Parse<Gender>(genderDisplay);
                items.Where(x => x.Gender == gender);
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                items.Where(x => x.EmployeeNo.Contains(q) || x.FirstName.Contains(q) || x.LastName.Contains(q));
            }

            return await items.OrderBy(x => x.EmployeeNo).ToListAsync();

        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateCompany(Company company)
        {

        }

        public void UpdateEmployee(Employee employee)
        {

        }
    }
}

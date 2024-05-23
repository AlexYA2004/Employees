using DbHandler.Entities;
using DbHandler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Services.ConcrateServices
{  
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> CreateEmployeeTable()
        {
            var result = await _context.Database.EnsureCreatedAsync();

            return result;
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();
        }

        public async Task CreateEmployees(IEnumerable<Employee> employees)
        {
            await _context.Employees.AddRangeAsync(employees);

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<Employee> GetAllEmployees()
        {
             return _context.Employees.OrderBy(e => e.FullName).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<Employee> GetFilteredEmployees()
        {
            return _context.Employees.Where(e => e.FullName.StartsWith("F") && e.Gender == "Male")
                .AsAsyncEnumerable();
        }

        public void OptimizeDatabase()
        {
            _context.Database.ExecuteSqlRaw("CREATE UNIQUE INDEX idx_gender_fullname ON Employees (FullName, Gender)");
        }

       

    }
}

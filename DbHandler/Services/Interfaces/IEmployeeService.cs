using DbHandler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployeeTable();

        Task CreateEmployee(Employee employee);

        Task CreateEmployees(IEnumerable<Employee> employees);

        IAsyncEnumerable<Employee> GetAllEmployees();

        IAsyncEnumerable<Employee> GetFilteredEmployees();

        void OptimizeDatabase();
    }
}
 
using Lily.Services.Configuration;
using Lily.Services.DbContexts;
using Lily.Services.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lily.Services.Services
{
    public class EmployeeDataManager: IEmployeeDataManager
    {
        private readonly EmployeeDbContext _dbContext;


        public EmployeeDataManager(
            IOptions<ServiceConfiguration> configuration,
            IMemoryCache memoryCache,
            EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Employee> GetById(Guid id)
        {
            return _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<Employee>> GetEmployees()
        {
            return _dbContext.Employees.ToListAsync();
        }

        public async Task SaveEmployees(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var employeeEntity = await GetById(employee.Id);
                if(employeeEntity == null)
                {
                    _dbContext.Add(employee);
                }
                else
                {
                    _dbContext.Entry(employeeEntity).CurrentValues.SetValues(employee);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}

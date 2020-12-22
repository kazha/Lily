using Lily.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Services
{
    public interface IEmployeeDataManager
    {
        Task<Employee> GetById(Guid id);
        Task<List<Employee>> GetEmployees();
        Task SaveEmployees(List<Employee> employees);
    }
}

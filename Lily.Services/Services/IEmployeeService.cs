using Lily.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
    }
}

using Lily.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeDataManager _dataManager;

        public EmployeeService(IEmployeeDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Task<List<Employee>> GetEmployees()
        {
            return _dataManager.GetEmployees();
        }
    }
}

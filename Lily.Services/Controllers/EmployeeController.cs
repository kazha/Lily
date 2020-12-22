using Lily.Services.Models;
using Lily.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;        
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeModel>> Get()
        {
            var employees = await _employeeService.GetEmployees();
            return employees.Select(e => (EmployeeModel)e).ToList();
        }

    }
}

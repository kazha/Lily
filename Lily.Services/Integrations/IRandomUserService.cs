using Lily.Services.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Integrations
{
    public interface IRandomUserService
    {
        Task<List<Employee>> GetExternalEmployees();
    }
}

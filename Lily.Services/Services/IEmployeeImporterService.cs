using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Services
{
    public interface IEmployeeImporterService
    {
        Task ImportFromRandomUser();
    }
}

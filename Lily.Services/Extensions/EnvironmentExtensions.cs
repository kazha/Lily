using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lily.Services.Extensions
{
    public static class EnvironmentExtensions
    {
        public const string UnitTestEnvironmentName = "UnitTest";

        public static bool IsUnitTest(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment(UnitTestEnvironmentName);
        }
    }
}

using Lily.Services.Configuration;
using Lily.Services.ExternalEntities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lily.Services.Integrations
{
    public class RandomUserService: IRandomUserService
    {
        private const string CacheKey = "EmployeeCache";

        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _client;

        public RandomUserService(
            IOptions<ServiceConfiguration> configuration,
            IMemoryCache memoryCache)
        {
            //Client should be alive all the time
            //therefore it should'nt be created each time requests are made
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.Value.EmployeeDataSource);
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Normally this probably would be separated
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<TResult> GetAsync<TResult>()
        {
            var httpResult = await _client.GetAsync("");
            var response = await httpResult.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(response);
        }

        /// <summary>
        /// Gets employee from memory cache,
        /// if none is found tries to get it from data source
        /// </summary>
        /// <returns>Cached employee list</returns>
        public async Task<List<Employee>> GetExternalEmployees()
        {
            List<Employee> employees;
            if (!_memoryCache.TryGetValue(CacheKey, out employees))
            {
                var result = await GetAsync<Root>();
                _memoryCache.Set(CacheKey, result.Results);
                employees = result.Results;
            }
            return employees;
        }
    }
}

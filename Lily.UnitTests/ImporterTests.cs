using Lily.Services;
using Lily.Services.Extensions;
using Lily.Services.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Text.Json;
using System.Collections.Generic;
using Lily.Services.Models;
using System.Linq;

namespace Lily.UnitTests
{
    public class ImporterTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ImporterTests()
        {
            var webHost = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment(EnvironmentExtensions.UnitTestEnvironmentName)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json", false, true);
                });
            _server = new TestServer(webHost);
            _client = _server.CreateClient();
        }
        [Fact]
        public async Task TestImport()
        {
            var response = await _client.GetAsync("/Employee");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<EmployeeModel>>(responseString);
            Assert.NotEmpty(result);
        }
    }
}

using Lily.Services.Configuration;
using Lily.Services.DbContexts;
using Lily.Services.Extensions;
using Lily.Services.Integrations;
using Lily.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lily.Services
{
    public class Startup
    {
        private readonly ServiceConfiguration _serviceConfiguration;

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _currentEnvironment = environment;
            _serviceConfiguration = Configuration.GetSection(ServiceConfiguration.ServiceConfigurationKey).Get<ServiceConfiguration>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.Configure<ServiceConfiguration>(Configuration.GetSection(ServiceConfiguration.ServiceConfigurationKey));

            services.AddDbContext<EmployeeDbContext>(options =>
            {
                if (_currentEnvironment.IsUnitTest())
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                }
                else
                {
                    options.UseSqlServer(Configuration.GetConnectionString(nameof(EmployeeDbContext)));
                }
            });

            services.AddScoped<IEmployeeService,EmployeeService>();
            services.AddScoped<IEmployeeDataManager, EmployeeDataManager>();

            //Integrations
            services.AddSingleton<IRandomUserService, RandomUserService>();
            services.AddTransient<IEmployeeImporterService, EmployeeImporterService>();
            services.AddSwaggerGen();
        }

        public void Configure(
            IApplicationBuilder app,
            IEmployeeImporterService employeeImporterService)
        {
            if (_currentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Loads employee data into memory
            //Saves data to local db
            employeeImporterService
                .ImportFromRandomUser()
                .GetAwaiter()
                .GetResult();
        }
    }
}

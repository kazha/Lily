using Lily.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lily.Services.DbContexts
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(p => p.Id);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}

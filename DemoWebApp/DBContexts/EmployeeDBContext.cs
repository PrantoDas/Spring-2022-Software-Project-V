using DemoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApp.DBContexts
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}

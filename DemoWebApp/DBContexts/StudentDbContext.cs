using DemoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApp.DBContexts
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}

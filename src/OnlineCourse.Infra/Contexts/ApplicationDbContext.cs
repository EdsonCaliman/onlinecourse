using Microsoft.EntityFrameworkCore;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Registrations;
using OnlineCourse.Domain.Students;

namespace OnlineCourse.Infra.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

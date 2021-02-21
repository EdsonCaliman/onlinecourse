using Microsoft.EntityFrameworkCore;
using OnlineCourse.Domain.Courses;
using OnlineCourse.Domain.Students;
using System.Threading.Tasks;

namespace OnlineCourse.Infra.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

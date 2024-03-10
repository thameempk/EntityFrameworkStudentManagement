using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Models
{
    public class StudentDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public StudentDbContext()
        {
        }
        public StudentDbContext(DbContextOptions<StudentDbContext> options ) : base( options )
        {
           
        }
        public DbSet<Students> students { get; set; }

        public DbSet<Course> courses { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId);
        }
    }
}

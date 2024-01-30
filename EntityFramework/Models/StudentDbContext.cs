using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Models
{
    public class StudentDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public StudentDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionString:DefaultConnection"];
        }
        public DbSet<Students> students { get; set; }

        public DbSet<Course> courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId);
        }
    }
}

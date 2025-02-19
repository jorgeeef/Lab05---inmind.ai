using Lab05.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lab05.Persistence;

public class UMSDbContext : DbContext
{
    public UMSDbContext(DbContextOptions<UMSDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Grade> Grades { get; set; }
}
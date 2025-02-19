using Lab05.Domain;
using Lab05.Domain.Enums;
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId });

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Enrollment)
            .WithMany(e => e.Grades)
            .HasForeignKey(g => g.EnrollmentId);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@ums.com",
            Role = UserRole.Admin
        });
    }
}
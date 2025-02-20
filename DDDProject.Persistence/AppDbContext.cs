using Lab05.Domain.Enums;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Persistence;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Grade> Grades { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite key for Enrollment
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId });

        // Relationships for Enrollment
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        // Relationship from Grade to Enrollment using composite keys
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Enrollment)
            .WithMany(e => e.Grades)
            .HasForeignKey(g => new { g.StudentId, g.CourseId });  // Use composite foreign key

        // Seeding initial data for User
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.Parse("c9c9e8b7-daa9-4209-8835-16ed2b01a47a"),  // Static Guid
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@ums.com",
            Role = UserRole.Admin
        });
    }

}
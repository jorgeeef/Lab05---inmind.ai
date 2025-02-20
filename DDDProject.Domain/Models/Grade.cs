namespace Lab05.Domain.Models;

public class Grade
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Foreign key for Enrollment, which is composite
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
    
    // Navigation property to Enrollment
    public Enrollment Enrollment { get; set; }

    public double Value { get; set; }
}
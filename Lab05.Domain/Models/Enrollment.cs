namespace Lab05.Domain;

public class Enrollment
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
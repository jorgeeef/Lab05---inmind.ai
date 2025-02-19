namespace Lab05.Domain;

public class Student: User
{
    public double AverageGrade { get; set; } = 0;
    public bool CanApplyToFrance { get; set; } = false;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
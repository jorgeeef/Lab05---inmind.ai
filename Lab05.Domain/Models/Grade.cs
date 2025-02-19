namespace Lab05.Domain;

public class Grade
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; }
    public double Value { get; set; }
}
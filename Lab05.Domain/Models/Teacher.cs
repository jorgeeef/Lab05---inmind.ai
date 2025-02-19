namespace Lab05.Domain;

public class Teacher : User
{
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
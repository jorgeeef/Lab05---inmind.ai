using MediatR;

namespace DDDProject.Application.Application.Courses.Commands;

public class CreateCourseCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public int MaxStudents { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
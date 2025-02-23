using MediatR;

namespace DDDProject.Application.Application.Grades.Commands;

public class SetGradeCommand : IRequest
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public double Value { get; set; }
}
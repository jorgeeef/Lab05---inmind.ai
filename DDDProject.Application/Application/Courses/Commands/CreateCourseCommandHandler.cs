using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;

namespace DDDProject.Application.Application.Courses.Commands;

using MediatR;


public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Name = request.Name,
            MaxStudents = request.MaxStudents,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await _courseRepository.AddAsync(course);
        return course.Id;
    }
}

using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Teachers.Commands;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Guid>
{
    private readonly ITeacherRepository _teacherRepository;

    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = new Teacher
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        await _teacherRepository.AddAsync(teacher);
        return teacher.Id;
    }
}
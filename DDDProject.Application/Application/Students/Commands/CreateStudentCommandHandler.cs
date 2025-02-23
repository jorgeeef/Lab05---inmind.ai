using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Students.Commands;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        await _studentRepository.AddAsync(student);
        return student.Id;
    }
}
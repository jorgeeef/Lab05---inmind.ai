using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Grades.Commands;

public class SetGradeCommandHandler
    : IRequestHandler<SetGradeCommand>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IStudentRepository _studentRepository;

    public SetGradeCommandHandler(IGradeRepository gradeRepository, IStudentRepository studentRepository)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
    }

    public async Task Handle(SetGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = new Grade
        {
            StudentId = request.StudentId,
            CourseId = request.CourseId,
            Value = request.Value
        };

        await _gradeRepository.AddAsync(grade);

        var student = await _studentRepository.GetByIdAsync(request.StudentId);
        student.AverageGrade = await _studentRepository.CalculateAverage(request.StudentId);
        student.CanApplyToFrance = student.AverageGrade > 15;

        await _studentRepository.UpdateAsync(student);
    }
}
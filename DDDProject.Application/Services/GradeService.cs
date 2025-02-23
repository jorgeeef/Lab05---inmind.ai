using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;

namespace DDDProject.Application.Services;

public class GradeService
{
    private readonly IGradeRepository _gradeRepository;

    public GradeService(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(Guid studentId)
    {
        return await _gradeRepository.GetGradesByStudentId(studentId);
    }
}

using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DDDProject.Application.Services;

public class GradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly ILogger<GradeService> _logger;
    private readonly SharedLocalizer _localizer;
    public GradeService(IGradeRepository gradeRepository, ILogger<GradeService> logger,SharedLocalizer localizer)
    {
        _gradeRepository = gradeRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(Guid studentId)
    {
        return await _gradeRepository.GetGradesByStudentId(studentId);
    }
    
}

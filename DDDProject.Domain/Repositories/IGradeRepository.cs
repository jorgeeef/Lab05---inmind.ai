using Lab05.Domain.Models;

namespace DDDProject.Domain.Repositories;

public interface IGradeRepository
{
    Task<IEnumerable<Grade>> GetGradesByStudentId(Guid studentId);
    Task AddAsync(Grade grade);
    Task<Grade?> GetByIdAsync(Guid id);
}
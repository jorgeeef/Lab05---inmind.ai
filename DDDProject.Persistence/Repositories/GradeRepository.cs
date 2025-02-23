using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Persistence.Repositories;

public class GradeRepository: IGradeRepository
{
    private readonly AppDbContext _context;

    public GradeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Grade>> GetGradesByStudentId(Guid studentId)
    {
        return await _context.Grades
            .Where(g => g.StudentId == studentId)
            .ToListAsync();
    }
}
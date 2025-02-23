using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Persistence.Repositories;

public class TeacherRepository: ITeacherRepository
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> GetByIdAsync(Guid id)
    {
        return await _context.Teachers.FindAsync(id);
    }
}
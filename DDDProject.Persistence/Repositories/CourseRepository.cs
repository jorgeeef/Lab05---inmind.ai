using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Persistence.Repositories;

public class CourseRepository: ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> GetByIdAsync(Guid id)
    {
        return await _context.Courses.FindAsync(id);
    }
    
}
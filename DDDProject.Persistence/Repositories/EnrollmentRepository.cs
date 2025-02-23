using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Persistence.Repositories;

public class EnrollmentRepository: IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments.ToListAsync();
    }

    public async Task<Enrollment> GetByIdAsync(Guid id)
    {
        return await _context.Enrollments.FindAsync(id);
    }
}
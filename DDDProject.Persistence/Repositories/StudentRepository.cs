using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        return await _context.Students.FindAsync(id);
    }
    
    public async Task<double> CalculateAverage(Guid studentId)
    {
        var grades = await _context.Grades
            .Where(g => g.StudentId == studentId)
            .ToListAsync();

        if (grades.Count == 0) return 0;

        return grades.Average(g => g.Value);
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }
}
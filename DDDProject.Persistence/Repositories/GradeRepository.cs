using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DDD.Persistence.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public GradeRepository(AppDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Grade>> GetGradesByStudentId(Guid studentId)
    {
        string cacheKey = $"Grades_Student_{studentId}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<IEnumerable<Grade>>(cachedData) ?? new List<Grade>();
        }

        var grades = await _context.Grades
            .Where(g => g.StudentId == studentId)
            .ToListAsync();

        if (grades.Any())
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(grades), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return grades;
    }

    public async Task<Grade?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"Grade_{id}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<Grade>(cachedData);
        }

        var grade = await _context.Grades.FindAsync(id);
        if (grade != null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(grade), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return grade;
    }

    public async Task AddAsync(Grade grade)
    {
        await _context.Grades.AddAsync(grade);
        await _context.SaveChangesAsync();
        
        // Invalidate cache to ensure fresh data
        await _cache.RemoveAsync($"Grades_Student_{grade.StudentId}");
    }
}

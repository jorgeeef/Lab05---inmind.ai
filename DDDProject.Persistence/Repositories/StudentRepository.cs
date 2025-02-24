using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DDD.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public StudentRepository(AppDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        // Invalidate cache
        await _cache.RemoveAsync("Students_All");
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        string cacheKey = "Students_All";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<IEnumerable<Student>>(cachedData) ?? new List<Student>();
        }

        var students = await _context.Students.ToListAsync();

        if (students.Any())
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(students), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return students;
    }

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"Student_{id}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<Student>(cachedData);
        }

        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(student), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return student;
    }

    public async Task<double> CalculateAverage(Guid studentId)
    {
        string cacheKey = $"Student_Avg_{studentId}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<double>(cachedData);
        }

        var grades = await _context.Grades
            .Where(g => g.StudentId == studentId)
            .ToListAsync();

        double average = grades.Count == 0 ? 0 : grades.Average(g => g.Value);

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(average), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        });

        return average;
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();

        // Invalidate cache
        await _cache.RemoveAsync($"Student_{student.Id}");
        await _cache.RemoveAsync("Students_All");
    }
}

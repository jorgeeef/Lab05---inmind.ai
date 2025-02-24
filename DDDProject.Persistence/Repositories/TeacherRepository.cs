using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DDD.Persistence.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public TeacherRepository(AppDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task AddAsync(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        // Invalidate cache
        await _cache.RemoveAsync("Teachers_All");
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        string cacheKey = "Teachers_All";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<IEnumerable<Teacher>>(cachedData) ?? new List<Teacher>();
        }

        var teachers = await _context.Teachers.ToListAsync();

        if (teachers.Any())
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(teachers), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return teachers;
    }

    public async Task<Teacher?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"Teacher_{id}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<Teacher>(cachedData);
        }

        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher != null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(teacher), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return teacher;
    }
}

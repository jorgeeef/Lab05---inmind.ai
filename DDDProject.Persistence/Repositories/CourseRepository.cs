using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DDD.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public CourseRepository(AppDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"Course_{id}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<Course>(cachedData);
        }

        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(course), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return course;
    }

    public async Task AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        
        // Invalidate cache for this course ID
        await _cache.RemoveAsync($"Course_{course.Id}");
    }
}
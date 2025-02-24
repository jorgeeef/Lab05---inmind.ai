using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DDD.Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public EnrollmentRepository(AppDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments.ToListAsync();
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"Enrollment_{id}";
        string? cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<Enrollment>(cachedData);
        }

        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment != null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(enrollment), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });
        }

        return enrollment;
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        
        // Invalidate cache to ensure fresh data
        string cacheKey = $"Enrollment_{enrollment.StudentId}_{enrollment.CourseId}";
        await _cache.RemoveAsync(cacheKey);
    }
}
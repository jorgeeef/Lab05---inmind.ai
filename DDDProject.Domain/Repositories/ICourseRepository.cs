using Lab05.Domain.Models;

namespace DDDProject.Domain.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> GetByIdAsync(Guid id);
}
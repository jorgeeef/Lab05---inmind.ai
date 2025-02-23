using Lab05.Domain.Models;

namespace DDDProject.Domain.Repositories;

public interface ITeacherRepository
{
    Task<IEnumerable<Teacher>> GetAllAsync();
    Task<Teacher> GetByIdAsync(Guid id);
}
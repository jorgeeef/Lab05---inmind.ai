using Lab05.Domain.Models;

namespace DDDProject.Domain.Repositories;

public interface IStudentRepository
{         
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(Guid id);
}
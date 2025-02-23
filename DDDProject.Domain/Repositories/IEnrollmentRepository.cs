using Lab05.Domain.Models;

namespace DDDProject.Domain.Repositories;

public interface IEnrollmentRepository
{
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment> GetByIdAsync(Guid id);
}
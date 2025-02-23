using Lab05.Domain.Models;

namespace DDDProject.Domain.Repositories;

public interface IStudentRepository
{         
    Task AddAsync(Student student);
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(Guid id);
    
    Task<double> CalculateAverage(Guid studentId);
    Task UpdateAsync(Student student);
}
using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;

namespace DDDProject.Application.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student> GetStudentByIdAsync(Guid id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }
}
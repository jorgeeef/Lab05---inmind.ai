using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;

namespace DDDProject.Application.Services;

public class TeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
    {
        return await _teacherRepository.GetAllAsync();
    }

    public async Task<Teacher> GetTeacherByIdAsync(Guid id)
    {
        return await _teacherRepository.GetByIdAsync(id);
    }
}
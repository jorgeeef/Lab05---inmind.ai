using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;

namespace DDDProject.Application.Services;

public class CourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllAsync();
    }

    public async Task<Course> GetCourseByIdAsync(Guid id)
    {
        return await _courseRepository.GetByIdAsync(id);
    }
}
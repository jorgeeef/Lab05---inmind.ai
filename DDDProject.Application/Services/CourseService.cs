using DDDProject.Domain.Repositories;
using DDDProject.Infrastructure.Localization;
using Lab05.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DDDProject.Application.Services;

public class CourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<CourseService> _logger;
    private readonly SharedLocalizer _localizer;

    public CourseService(
        ICourseRepository courseRepository,
        ILogger<CourseService> logger,
        SharedLocalizer localizer)
    {
        _courseRepository = courseRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        _logger.LogInformation(_localizer["FetchingAllCourses"]);
        
        try
        {
            var courses = await _courseRepository.GetAllAsync();
            _logger.LogInformation(_localizer["SuccessfullyFetchedCourses"], courses.Count());
            return courses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, _localizer["ErrorFetchingCourses"]);
            throw;
        }
    }

    public async Task<Course> GetCourseByIdAsync(Guid id)
    {
        _logger.LogInformation(_localizer["FetchingCourseById"], id);
        
        try
        {
            var course = await _courseRepository.GetByIdAsync(id);
            
            if (course == null)
            {
                _logger.LogWarning(_localizer["CourseNotFound"], id);
                throw new KeyNotFoundException(_localizer["CourseNotFound"]);
            }
            
            _logger.LogInformation(_localizer["SuccessfullyFetchedCourse"], id);
            return course;
        }
        catch (Exception ex) when (!(ex is KeyNotFoundException))
        {
            _logger.LogError(ex, _localizer["ErrorFetchingCourseById"], id);
            throw;
        }
    }
}
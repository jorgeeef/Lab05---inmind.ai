using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.Extensions.Logging;
using DDDProject.Application.Localization; // Import SharedLocalizer


namespace DDDProject.Application.Services;

public class TeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILogger<TeacherService> _logger;
    private readonly SharedLocalizer _localizer;

    public TeacherService(ITeacherRepository teacherRepository, ILogger<TeacherService> logger, SharedLocalizer localizer)
    {
        _teacherRepository = teacherRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
    {
        _logger.LogInformation(_localizer["Fetching all teachers"]);

        var teachers = await _teacherRepository.GetAllAsync();

        _logger.LogInformation(_localizer["Fetched {Count} teachers"], teachers.Count());

        return teachers;
    }

    public async Task<Teacher?> GetTeacherByIdAsync(Guid id)
    {
        _logger.LogInformation(_localizer["Fetching teacher with ID {Id}"], id);

        var teacher = await _teacherRepository.GetByIdAsync(id);

        if (teacher == null)
        {
            _logger.LogWarning(_localizer["Teacher with ID {Id} not found"], id);
        }
        else
        {
            _logger.LogInformation(_localizer["Teacher with ID {Id} found"], id);
        }

        return teacher;
    }
}
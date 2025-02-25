using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.Extensions.Logging;
using DDDProject.Application.Localization; // Import SharedLocalizer

namespace DDDProject.Application.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<StudentService> _logger;
    private readonly SharedLocalizer _localizer;

    public StudentService(IStudentRepository studentRepository, ILogger<StudentService> logger, SharedLocalizer localizer)
    {
        _studentRepository = studentRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        _logger.LogInformation(_localizer["Fetching all students"]);
        
        var students = await _studentRepository.GetAllAsync();

        _logger.LogInformation(_localizer["Fetched {Count} students"], students.Count());
        
        return students;
    }

    public async Task<Student?> GetStudentByIdAsync(Guid id)
    {
        _logger.LogInformation(_localizer["Fetching student with ID {Id}"], id);
        
        var student = await _studentRepository.GetByIdAsync(id);

        if (student == null)
        {
            _logger.LogWarning(_localizer["Student with ID {Id} not found"], id);
        }
        else
        {
            _logger.LogInformation(_localizer["Student with ID {Id} found"], id);
        }

        return student;
    }
}
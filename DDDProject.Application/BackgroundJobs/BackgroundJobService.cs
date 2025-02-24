using DDDProject.Domain.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DDDProject.Application.BackgroundJobs;

public class BackgroundJobService : IBackgroundJobService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<BackgroundJobService> _logger;
    private readonly IStringLocalizer<BackgroundJobService> _localizer;

    public BackgroundJobService(
        IStudentRepository studentRepository,
        ILogger<BackgroundJobService> logger,
        IStringLocalizer<BackgroundJobService> localizer)
    {
        _studentRepository = studentRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task RecalculateStudentAverages()
    {
        _logger.LogInformation(_localizer["Recalculating student grade averages..."]);

        var students = await _studentRepository.GetAllWithGradesAsync();
        foreach (var student in students)
        {
            var grades = student.Enrollments.SelectMany(e => e.Grades).ToList();
            double newAverage = grades.Any() ? grades.Average(g => g.Value) : 0;

            student.AverageGrade = newAverage;
            
            await _studentRepository.UpdateAsync(student);

            _logger.LogDebug($"Updated student {student.Id} - {student.FirstName} {student.LastName}: New Avg = {newAverage}");
        }

        _logger.LogInformation(_localizer["Student grade averages updated successfully."]);
    }

    public async Task SendEnrollmentDeadlineNotifications()
    {
        _logger.LogInformation(_localizer["Sending enrollment deadline notifications..."]);

        var students = await _studentRepository.GetAllAsync();
        foreach (var student in students)
        {
            foreach (var enrollment in student.Enrollments)
            {
                if ((enrollment.Course.StartDate - DateTime.UtcNow).TotalDays <= 7)
                {
                    _logger.LogDebug($"Reminder sent to {student.Email} for Course: {enrollment.Course.Name}");
                    // I need to modify it later to send an email instead
                }
                
            }
        }

        _logger.LogInformation(_localizer["Enrollment deadline notifications sent."]);
    }
}

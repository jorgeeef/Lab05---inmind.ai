using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using Microsoft.Extensions.Logging;
using DDDProject.Application.Localization; // Import SharedLocalizer

namespace DDDProject.Application.Services;

public class EnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ILogger<EnrollmentService> _logger;
    private readonly SharedLocalizer _localizer;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, ILogger<EnrollmentService> logger, SharedLocalizer localizer)
    {
        _enrollmentRepository = enrollmentRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        _logger.LogInformation(_localizer["Fetching all enrollments"]);

        var enrollments = await _enrollmentRepository.GetAllAsync();

        _logger.LogInformation(_localizer["Fetched {Count} enrollments"], enrollments.Count());

        return enrollments;
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(Guid id)
    {
        _logger.LogInformation(_localizer["Fetching enrollment with ID {Id}"], id);

        var enrollment = await _enrollmentRepository.GetByIdAsync(id);

        if (enrollment == null)
        {
            _logger.LogWarning(_localizer["Enrollment with ID {Id} not found"], id);
        }
        else
        {
            _logger.LogInformation(_localizer["Enrollment with ID {Id} found"], id);
        }

        return enrollment;
    }
}
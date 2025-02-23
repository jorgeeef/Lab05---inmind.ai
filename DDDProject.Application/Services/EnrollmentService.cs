using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;

namespace DDDProject.Application.Services;

public class EnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await _enrollmentRepository.GetAllAsync();
    }

    public async Task<Enrollment> GetEnrollmentByIdAsync(Guid id)
    {
        return await _enrollmentRepository.GetByIdAsync(id);
    }
}
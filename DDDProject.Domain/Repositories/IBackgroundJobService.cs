namespace DDDProject.Domain.Repositories;

public interface IBackgroundJobService
{
    Task RecalculateStudentAverages();
    Task SendEnrollmentDeadlineNotifications();
}
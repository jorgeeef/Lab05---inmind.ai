using DDD.Persistence.Repositories;
using DDDProject.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DDDProject.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        return services;
    }
}
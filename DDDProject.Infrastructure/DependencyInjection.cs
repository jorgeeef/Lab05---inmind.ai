using DDD.Persistence.Repositories;
using DDDProject.Application.Services;
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
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CourseService>();
        services.AddScoped<StudentService>();
        services.AddScoped<EnrollmentService>();
        services.AddScoped<TeacherService>();
        services.AddScoped<GradeService>();
        return services;
    }
}
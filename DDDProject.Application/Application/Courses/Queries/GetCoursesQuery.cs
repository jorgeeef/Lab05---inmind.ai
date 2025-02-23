using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Courses.Queries;

public class GetCoursesQuery: IRequest<List<Course>> 
{
    
}
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Students.Queries;

public class GetStudentsQuery : IRequest<List<Student>> { }
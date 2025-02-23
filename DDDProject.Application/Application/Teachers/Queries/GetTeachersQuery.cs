using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Teachers.Queries;

public class GetTeachersQuery : IRequest<List<Teacher>> { }
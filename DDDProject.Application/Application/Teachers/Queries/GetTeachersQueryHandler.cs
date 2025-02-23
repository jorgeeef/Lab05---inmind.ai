using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Teachers.Queries;

public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, List<Teacher>>
{
    private readonly ITeacherRepository _teacherRepository;

    public GetTeachersQueryHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<List<Teacher>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        return (await _teacherRepository.GetAllAsync()).ToList();
    }
}
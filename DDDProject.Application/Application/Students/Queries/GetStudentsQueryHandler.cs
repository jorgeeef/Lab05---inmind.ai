using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Students.Queries;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<Student>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        return (await _studentRepository.GetAllAsync()).ToList();
    }
}
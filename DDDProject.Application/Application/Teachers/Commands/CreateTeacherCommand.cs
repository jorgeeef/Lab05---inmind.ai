using MediatR;

namespace DDDProject.Application.Application.Teachers.Commands;

public class CreateTeacherCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
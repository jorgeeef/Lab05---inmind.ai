using MediatR;

namespace DDDProject.Application.Application.Students.Commands;

public class CreateStudentCommand: IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
}

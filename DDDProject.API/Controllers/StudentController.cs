using DDDProject.Application.Application.Students.Commands;
using DDDProject.Application.Application.Students.Queries;
using DDDProject.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;
[ApiController]
[Route("api/students")]
public class StudentController:ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
    {
        var studentId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = studentId }, studentId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _mediator.Send(new GetStudentsQuery());
        return Ok(students);
    }

}
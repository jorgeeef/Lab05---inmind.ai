using DDDProject.Application.Application.Teachers.Commands;
using DDDProject.Application.Application.Teachers.Queries;
using DDDProject.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeacherController:ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacherCommand command)
    {
        var teacherId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = teacherId }, teacherId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _mediator.Send(new GetTeachersQuery());
        return Ok(teachers);
    }

}
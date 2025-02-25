using DDDProject.Application.Application.Students.Commands;
using DDDProject.Application.Application.Students.Queries;
using DDDProject.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDDProject.Infrastructure.Localization; // Import SharedLocalizer

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<StudentController> _logger;
    private readonly SharedLocalizer _localizer;

    public StudentController(IMediator mediator, ILogger<StudentController> logger, SharedLocalizer localizer)
    {
        _mediator = mediator;
        _logger = logger;
        _localizer = localizer;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
    {
        _logger.LogInformation(_localizer["Creating a new student"]);

        var studentId = await _mediator.Send(command);

        _logger.LogInformation(_localizer["Student created successfully with ID {Id}"], studentId);

        return CreatedAtAction(nameof(GetAll), new { id = studentId }, studentId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation(_localizer["Fetching all students"]);

        var students = await _mediator.Send(new GetStudentsQuery());

        _logger.LogInformation(_localizer["Fetched {Count} students"], students.Count());

        return Ok(students);
    }
}
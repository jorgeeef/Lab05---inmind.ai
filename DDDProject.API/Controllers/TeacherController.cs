using DDDProject.Application.Application.Teachers.Commands;
using DDDProject.Application.Application.Teachers.Queries;
using DDDProject.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDDProject.Infrastructure.Localization; // Import SharedLocalizer

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TeacherController> _logger;
    private readonly SharedLocalizer _localizer;

    public TeacherController(IMediator mediator, ILogger<TeacherController> logger, SharedLocalizer localizer)
    {
        _mediator = mediator;
        _logger = logger;
        _localizer = localizer;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacherCommand command)
    {
        _logger.LogInformation(_localizer["Creating a new teacher"]);

        var teacherId = await _mediator.Send(command);

        _logger.LogInformation(_localizer["Teacher created successfully with ID {Id}"], teacherId);

        return CreatedAtAction(nameof(GetAll), new { id = teacherId }, teacherId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation(_localizer["Fetching all teachers"]);

        var teachers = await _mediator.Send(new GetTeachersQuery());

        _logger.LogInformation(_localizer["Fetched {Count} teachers"], teachers.Count());

        return Ok(teachers);
    }
}
using DDDProject.Application.Application.Courses.Commands;
using DDDProject.Application.Application.Courses.Queries;
using DDDProject.Application.Services;
using DDDProject.Infrastructure.Localization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CourseController> _logger;
    private readonly SharedLocalizer _localizer;

    public CourseController(
        IMediator mediator,
        ILogger<CourseController> logger,
        SharedLocalizer localizer)
    {
        _mediator = mediator;
        _logger = logger;
        _localizer = localizer;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
    {
        _logger.LogInformation(_localizer["AttemptingToCreateCourse"], command.Name);
        
        try
        {
            var courseId = await _mediator.Send(command);
            _logger.LogInformation(_localizer["CourseCreatedSuccessfully"], courseId);
            return CreatedAtAction(nameof(GetAll), new { id = courseId }, courseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, _localizer["ErrorCreatingCourse"]);
            return StatusCode(500, new { message = _localizer["InternalServerError"] });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation(_localizer["AttemptingToFetchAllCourses"]);
        
        try
        {
            var courses = await _mediator.Send(new GetCoursesQuery());
            _logger.LogInformation(_localizer["SuccessfullyFetchedAllCourses"], courses.Count());
            return Ok(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, _localizer["ErrorFetchingAllCourses"]);
            return StatusCode(500, new { message = _localizer["InternalServerError"] });
        }
    }
}
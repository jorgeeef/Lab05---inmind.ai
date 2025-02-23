using DDDProject.Application.Application.Courses.Commands;
using DDDProject.Application.Application.Courses.Queries;
using DDDProject.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController: ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
    {
        var courseId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = courseId }, courseId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _mediator.Send(new GetCoursesQuery());
        return Ok(courses);
    }

}
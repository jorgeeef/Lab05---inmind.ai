using DDDProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController: ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }
    


}
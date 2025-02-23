using DDDProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeacherController:ControllerBase
{
    private readonly TeacherService _teacherService;

    public TeacherController(TeacherService teacherService)
    {
        _teacherService = teacherService;
    }

}
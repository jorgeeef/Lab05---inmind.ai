using DDDProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;
[ApiController]
[Route("api/students")]
public class StudentController:ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }


}
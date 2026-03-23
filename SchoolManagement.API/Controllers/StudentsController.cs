using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Students.Commands.CreateStudent;
using SchoolManagement.Application.Features.Students.Commands.EnrollStudentToCourse;
using SchoolManagement.Application.Features.Students.Queries.GetStudents;

namespace SchoolManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar alumnos
    /// Permite crear alumnos, matricularlos a cursos y consultar información
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] bool? onlyActive = null,
            [FromQuery] Guid? courseId = null)
        {
            var query = new GetStudentsQuery
            {
                OnlyActive = onlyActive,
                CourseId = courseId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Alumno creado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("enroll")]
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> EnrollToCourse([FromBody] EnrollStudentToCourseCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Alumno matriculado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Courses.Commands.AssignSubjectsToCourse;
using SchoolManagement.Application.Features.Courses.Commands.CreateCourse;
using SchoolManagement.Application.Features.Courses.Queries.GetCourseDetail;
using SchoolManagement.Application.Features.Courses.Queries.GetCourses;

namespace SchoolManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar cursos
    /// Un curso es la combinación de grado + sección + año académico
    /// Ejemplo: 1° A 2024 (Primer Grado, Sección A, del año 2024)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene la lista de cursos
        /// Puede filtrar por año académico y/o grado
        /// </summary>
        /// <param name="academicYearId">Filtro opcional: ID del año académico</param>
        /// <param name="gradeId">Filtro opcional: ID del grado</param>
        /// <returns>Lista de cursos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid? academicYearId = null,
            [FromQuery] Guid? gradeId = null)
        {
            var query = new GetCoursesQuery
            {
                AcademicYearId = academicYearId,
                GradeId = gradeId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el detalle completo de un curso
        /// Incluye año académico, grado, sección, materias y alumnos matriculados
        /// </summary>
        /// <param name="id">ID del curso</param>
        /// <returns>Detalle completo del curso</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var query = new GetCourseDetailQuery { CourseId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { message = "Curso no encontrado" });

            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo curso
        /// Solo administradores pueden crear cursos
        /// </summary>
        /// <param name="command">Datos del curso a crear</param>
        /// <returns>ID del curso creado</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Curso creado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Asigna materias a un curso
        /// Solo administradores pueden asignar materias
        /// </summary>
        /// <param name="command">ID del curso y lista de IDs de materias</param>
        /// <returns>Confirmación de asignación</returns>
        [HttpPost("assign-subjects")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignSubjects([FromBody] AssignSubjectsToCourseCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Materias asignadas exitosamente al curso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Teachers.Commands.AssignTeacherToSubject;
using SchoolManagement.Application.Features.Teachers.Commands.CreateTeacher;
using SchoolManagement.Application.Features.Teachers.Queries.GetTeacherDetail;
using SchoolManagement.Application.Features.Teachers.Queries.GetTeachers;

namespace SchoolManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar profesores
    /// Permite crear profesores, asignarlos a materias y consultar su información
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene la lista de profesores
        /// </summary>
        /// <param name="onlyActive">Si es true, solo trae profesores activos</param>
        /// <returns>Lista de profesores</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? onlyActive = null)
        {
            var query = new GetTeachersQuery { OnlyActive = onlyActive };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el detalle completo de un profesor
        /// Incluye sus asignaciones a cursos y materias
        /// </summary>
        /// <param name="id">ID del profesor</param>
        /// <returns>Detalle completo del profesor</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var query = new GetTeacherDetailQuery { TeacherId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { message = "Profesor no encontrado" });

            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo profesor
        /// Solo administradores y secretarias pueden crear profesores
        /// </summary>
        /// <param name="command">Datos del profesor a crear</param>
        /// <returns>ID del profesor creado</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Create([FromBody] CreateTeacherCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Profesor creado exitosamente" });
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
        /// Asigna un profesor a una materia de un curso
        /// Solo administradores pueden hacer asignaciones
        /// </summary>
        /// <param name="command">Datos de la asignación</param>
        /// <returns>ID de la asignación creada</returns>
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignToSubject([FromBody] AssignTeacherToSubjectCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Profesor asignado exitosamente" });
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

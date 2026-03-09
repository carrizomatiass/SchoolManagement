using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Grades.Commands.CreateGrade;
using SchoolManagement.Application.Features.Grades.Queries.GetGrades;

namespace SchoolManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar grados escolares
    /// Permite crear y consultar los grados (1° a 6°) del colegio
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GradesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene la lista de grados
        /// </summary>
        /// <param name="level">Filtro opcional por nivel: "primary" o "secondary"</param>
        /// <returns>Lista de grados ordenados por número</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? level = null)
        {
            var query = new GetGradesQuery { Level = level };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo grado
        /// Solo administradores pueden crear grados
        /// </summary>
        /// <param name="command">Datos del grado a crear</param>
        /// <returns>ID del grado creado</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateGradeCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Grado creado exitosamente" });
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

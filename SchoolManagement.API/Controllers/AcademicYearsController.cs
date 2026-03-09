using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.AcademicYears.Commands.CreateAcademicYear;
using SchoolManagement.Application.Features.AcademicYears.Queries.GetAcademicYears;

namespace SchoolManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar años académicos
    /// Permite crear, consultar y administrar los períodos académicos del colegio
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class AcademicYearsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AcademicYearsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene la lista de años académicos
        /// </summary>
        /// <param name="onlyActive">Si es true, solo trae años académicos activos</param>
        /// <returns>Lista de años académicos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? onlyActive = null)
        {
            var query = new GetAcademicYearsQuery { OnlyActive = onlyActive };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo año académico
        /// Solo administradores pueden crear años académicos
        /// </summary>
        /// <param name="command">Datos del año académico a crear</param>
        /// <returns>ID del año académico creado</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")] // Solo administradores
        public async Task<IActionResult> Create([FromBody] CreateAcademicYearCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { id, message = "Año académico creado exitosamente" });
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

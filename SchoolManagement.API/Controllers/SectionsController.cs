using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Sections.Commands.CreateSection;
using SchoolManagement.Application.Features.Sections.Queries.GetSections;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// Controlador para gestionar secciones
/// Las secciones dividen los cursos dentro de un mismo grado (A, B, C, etc.)
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todas las secciones
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetSectionsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Crea una nueva sección
    /// Solo administradores
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSectionCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(new { id, message = "Sección creada exitosamente" });
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

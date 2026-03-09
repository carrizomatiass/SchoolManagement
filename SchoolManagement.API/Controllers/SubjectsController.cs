using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Subjects.Commands.CreateSubject;
using SchoolManagement.Application.Features.Subjects.Queries.GetSubjects;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// Controlador para gestionar materias
/// Permite crear y consultar las materias del colegio (Matemática, Lengua, etc.)
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todas las materias
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetSubjectsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Crea una nueva materia
    /// Solo administradores
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSubjectCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(new { id, message = "Materia creada exitosamente" });
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

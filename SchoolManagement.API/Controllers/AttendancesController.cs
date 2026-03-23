using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Attendances.Commands.CreateAttendance;
using SchoolManagement.Application.Features.Attendances.Queries.GetAttendancesByStudent;
using SchoolManagement.Application.Features.Attendances.Queries.GetAttendancesByCourse;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// Controlador para gestionar asistencias - Permite tomar asistencia y consultar por alumno o curso
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendancesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttendancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<List<object>>> GetAttendancesByStudent(
        Guid studentId,
        [FromQuery] Guid? courseId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var query = new GetAttendancesByStudentQuery
        {
            StudentId = studentId,
            CourseId = courseId,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("course")]
    public async Task<ActionResult<List<object>>> GetAttendancesByCourse(
        [FromQuery] Guid courseId,
        [FromQuery] DateTime date,
        CancellationToken cancellationToken)
    {
        var query = new GetAttendancesByCourseQuery
        {
            CourseId = courseId,
            Date = date
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<ActionResult<Guid>> CreateAttendance(
        [FromBody] CreateAttendanceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAttendancesByStudent), new { studentId = command.StudentId }, result);
    }
}

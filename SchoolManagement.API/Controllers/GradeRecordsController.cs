using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Grades.Commands.CreateGradeRecord;
using SchoolManagement.Application.Features.Grades.Queries.GetGradeRecordsByStudent;
using SchoolManagement.Application.Features.Grades.Queries.GetGradeRecordsByCourse;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// Controlador para gestionar calificaciones - Permite a profesores cargar notas y consultar por alumno o curso
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GradeRecordsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GradeRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<List<object>>> GetGradeRecordsByStudent(
        Guid studentId,
        [FromQuery] Guid? termId,
        CancellationToken cancellationToken)
    {
        var query = new GetGradeRecordsByStudentQuery
        {
            StudentId = studentId,
            TermId = termId
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("course")]
    public async Task<ActionResult<List<object>>> GetGradeRecordsByCourse(
        [FromQuery] Guid courseId,
        [FromQuery] Guid subjectId,
        [FromQuery] Guid termId,
        CancellationToken cancellationToken)
    {
        var query = new GetGradeRecordsByCourseQuery
        {
            CourseId = courseId,
            SubjectId = subjectId,
            TermId = termId
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<ActionResult<Guid>> CreateGradeRecord(
        [FromBody] CreateGradeRecordCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetGradeRecordsByStudent), new { studentId = command.StudentId }, result);
    }
}

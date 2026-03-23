using MediatR;
using SchoolManagement.Application.Features.Grades.Models;

namespace SchoolManagement.Application.Features.Grades.Queries.GetGradeRecordsByStudent;

/// <summary>
/// Query para obtener calificaciones de un alumno - Permite filtrar por trimestre
/// </summary>
public class GetGradeRecordsByStudentQuery : IRequest<List<GradeRecordDto>>
{
    public Guid StudentId { get; set; }
    public Guid? TermId { get; set; }
}

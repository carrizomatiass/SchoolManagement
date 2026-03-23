using MediatR;
using SchoolManagement.Application.Features.Grades.Models;

namespace SchoolManagement.Application.Features.Grades.Queries.GetGradeRecordsByCourse;

/// <summary>
/// Query para obtener calificaciones de un curso por materia y trimestre
/// </summary>
public class GetGradeRecordsByCourseQuery : IRequest<List<GradeRecordDto>>
{
    public Guid CourseId { get; set; }
    public Guid SubjectId { get; set; }
    public Guid TermId { get; set; }
}

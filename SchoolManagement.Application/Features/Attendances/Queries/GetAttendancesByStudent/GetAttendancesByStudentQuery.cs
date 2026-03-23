using MediatR;
using SchoolManagement.Application.Features.Attendances.Models;

namespace SchoolManagement.Application.Features.Attendances.Queries.GetAttendancesByStudent;

/// <summary>
/// Query para obtener asistencias de un alumno con filtros opcionales
/// </summary>
public class GetAttendancesByStudentQuery : IRequest<List<AttendanceDto>>
{
    public Guid StudentId { get; set; }
    public Guid? CourseId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

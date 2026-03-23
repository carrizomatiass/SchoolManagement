using MediatR;
using SchoolManagement.Application.Features.Attendances.Models;

namespace SchoolManagement.Application.Features.Attendances.Queries.GetAttendancesByCourse;

/// <summary>
/// Query para obtener asistencias de un curso en una fecha específica
/// </summary>
public class GetAttendancesByCourseQuery : IRequest<List<AttendanceDto>>
{
    public Guid CourseId { get; set; }
    public DateTime Date { get; set; }
}

using MediatR;

namespace SchoolManagement.Application.Features.Attendances.Commands.CreateAttendance;

/// <summary>
/// Comando para registrar asistencia de un alumno
/// </summary>
public class CreateAttendanceCommand : IRequest<Guid>
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
    public string? Comments { get; set; }
    public Guid CreatedBy { get; set; }
}

using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Attendances.Models;

public class AttendanceDto
{
    public Guid Id { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public AttendanceStatus Status { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
}

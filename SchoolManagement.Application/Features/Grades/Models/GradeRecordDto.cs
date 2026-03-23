using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Grades.Models;

public class GradeRecordDto
{
    public Guid Id { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public string TermName { get; set; } = string.Empty;
    public decimal GradeValue { get; set; }
    public GradeStatus Status { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
}

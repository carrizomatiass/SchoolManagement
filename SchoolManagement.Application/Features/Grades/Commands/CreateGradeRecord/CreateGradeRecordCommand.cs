using MediatR;

namespace SchoolManagement.Application.Features.Grades.Commands.CreateGradeRecord;

/// <summary>
/// Comando para cargar una calificación de un alumno en una materia
/// </summary>
public class CreateGradeRecordCommand : IRequest<Guid>
{
    public Guid StudentId { get; set; }
    public Guid CourseSubjectId { get; set; }
    public Guid TermId { get; set; }
    public decimal GradeValue { get; set; }
    public string? Comments { get; set; }
    public Guid CreatedBy { get; set; }
}

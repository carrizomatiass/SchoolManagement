using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Grades.Commands.CreateGradeRecord;

/// <summary>
/// Handler que carga una calificación - Calcula automáticamente si aprobó o desaprobó
/// </summary>
public class CreateGradeRecordCommandHandler : IRequestHandler<CreateGradeRecordCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateGradeRecordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateGradeRecordCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"El alumno con ID {request.StudentId} no existe");

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == request.StudentId, cancellationToken);

        if (enrollment is null)
            throw new InvalidOperationException($"El alumno no está matriculado en ningún curso");

        var existingGrade = await _context.GradeRecords
            .FirstOrDefaultAsync(g => 
                g.StudentId == request.StudentId && 
                g.CourseSubjectId == request.CourseSubjectId && 
                g.TermId == request.TermId, 
                cancellationToken);

        if (existingGrade is not null)
            throw new InvalidOperationException("Ya existe una calificación para este alumno, materia y trimestre");

        var status = request.GradeValue >= 6 ? GradeStatus.Approved : GradeStatus.Failed;

        var gradeRecord = new GradeRecord
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            CourseSubjectId = request.CourseSubjectId,
            TermId = request.TermId,
            GradeValue = request.GradeValue,
            Status = status,
            Comments = request.Comments,
            CreatedBy = request.CreatedBy,
            CreatedAt = DateTime.UtcNow
        };

        _context.GradeRecords.Add(gradeRecord);
        await _context.SaveChangesAsync(cancellationToken);

        return gradeRecord.Id;
    }
}

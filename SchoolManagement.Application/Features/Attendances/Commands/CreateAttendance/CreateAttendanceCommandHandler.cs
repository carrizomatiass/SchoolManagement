using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Attendances.Commands.CreateAttendance;

/// <summary>
/// Handler que registra asistencia - Verifica que el alumno esté matriculado en el curso
/// </summary>
public class CreateAttendanceCommandHandler : IRequestHandler<CreateAttendanceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAttendanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"El alumno con ID {request.StudentId} no existe");

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => 
                e.StudentId == request.StudentId && 
                e.CourseId == request.CourseId, 
                cancellationToken);

        if (enrollment is null)
            throw new InvalidOperationException("El alumno no está matriculado en este curso");

        var existingAttendance = await _context.Attendances
            .FirstOrDefaultAsync(a => 
                a.StudentId == request.StudentId && 
                a.CourseId == request.CourseId && 
                a.Date.Date == request.Date.Date, 
                cancellationToken);

        if (existingAttendance is not null)
            throw new InvalidOperationException("Ya existe un registro de asistencia para este alumno en esta fecha y curso");

        var attendance = new Attendance
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            CourseId = request.CourseId,
            Date = request.Date,
            Status = (AttendanceStatus)request.Status,
            Comments = request.Comments,
            CreatedBy = request.CreatedBy,
            CreatedAt = DateTime.UtcNow
        };

        _context.Attendances.Add(attendance);
        await _context.SaveChangesAsync(cancellationToken);

        return attendance.Id;
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Attendances.Models;

namespace SchoolManagement.Application.Features.Attendances.Queries.GetAttendancesByStudent;

/// <summary>
/// Handler que obtiene asistencias de un alumno con filtros por curso y rango de fechas
/// </summary>
public class GetAttendancesByStudentQueryHandler : IRequestHandler<GetAttendancesByStudentQuery, List<AttendanceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAttendancesByStudentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AttendanceDto>> Handle(GetAttendancesByStudentQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Attendances
            .Where(a => a.StudentId == request.StudentId);

        if (request.CourseId.HasValue)
            query = query.Where(a => a.CourseId == request.CourseId.Value);

        if (request.StartDate.HasValue)
            query = query.Where(a => a.Date.Date >= request.StartDate.Value.Date);

        if (request.EndDate.HasValue)
            query = query.Where(a => a.Date.Date <= request.EndDate.Value.Date);

        var attendances = await query
            .Include(a => a.Student)
                .ThenInclude(s => s.User!)
                    .ThenInclude(u => u.Profile)
            .Include(a => a.Course)
            .OrderByDescending(a => a.Date)
            .Select(a => new AttendanceDto
            {
                Id = a.Id,
                StudentName = a.Student!.User!.Profile!.FirstName + " " + a.Student.User.Profile.LastName,
                CourseName = a.Course!.Name,
                Date = a.Date,
                Status = a.Status,
                Comments = a.Comments,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return attendances;
    }
}

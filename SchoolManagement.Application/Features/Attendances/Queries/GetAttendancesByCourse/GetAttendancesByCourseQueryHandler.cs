using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Attendances.Models;

namespace SchoolManagement.Application.Features.Attendances.Queries.GetAttendancesByCourse;

/// <summary>
/// Handler que obtiene asistencias de todos los alumnos de un curso en una fecha
/// </summary>
public class GetAttendancesByCourseQueryHandler : IRequestHandler<GetAttendancesByCourseQuery, List<AttendanceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAttendancesByCourseQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AttendanceDto>> Handle(GetAttendancesByCourseQuery request, CancellationToken cancellationToken)
    {
        var attendances = await _context.Attendances
            .Where(a => a.CourseId == request.CourseId && a.Date.Date == request.Date.Date)
            .Include(a => a.Student)
                .ThenInclude(s => s.User!)
                    .ThenInclude(u => u.Profile)
            .Include(a => a.Course)
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

        return attendances.OrderBy(a => a.StudentName).ToList();
    }
}

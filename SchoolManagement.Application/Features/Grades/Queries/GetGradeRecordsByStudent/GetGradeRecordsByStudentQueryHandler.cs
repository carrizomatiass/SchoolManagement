using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Grades.Models;

namespace SchoolManagement.Application.Features.Grades.Queries.GetGradeRecordsByStudent;

/// <summary>
/// Handler que obtiene las calificaciones de un alumno con filtro opcional por trimestre
/// </summary>
public class GetGradeRecordsByStudentQueryHandler : IRequestHandler<GetGradeRecordsByStudentQuery, List<GradeRecordDto>>
{
    private readonly IApplicationDbContext _context;

    public GetGradeRecordsByStudentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GradeRecordDto>> Handle(GetGradeRecordsByStudentQuery request, CancellationToken cancellationToken)
    {
        var query = _context.GradeRecords
            .Where(g => g.StudentId == request.StudentId);

        if (request.TermId.HasValue)
            query = query.Where(g => g.TermId == request.TermId.Value);

        var gradeRecords = await query
            .Include(g => g.Student)
                .ThenInclude(s => s.User!)
                    .ThenInclude(u => u.Profile)
            .Include(g => g.CourseSubject)
                .ThenInclude(cs => cs.Subject)
            .Include(g => g.CourseSubject)
                .ThenInclude(cs => cs.Course)
            .Include(g => g.Term)
            .OrderBy(g => g.Term!.Name)
            .ThenBy(g => g.CourseSubject!.Subject!.Name)
            .Select(g => new GradeRecordDto
            {
                Id = g.Id,
                StudentName = g.Student!.User!.Profile!.FirstName + " " + g.Student.User.Profile.LastName,
                SubjectName = g.CourseSubject!.Subject!.Name,
                CourseName = g.CourseSubject.Course!.Name,
                TermName = g.Term!.Name,
                GradeValue = g.GradeValue,
                Status = g.Status,
                Comments = g.Comments,
                CreatedAt = g.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return gradeRecords;
    }
}

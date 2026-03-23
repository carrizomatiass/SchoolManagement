using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Grades.Models;

namespace SchoolManagement.Application.Features.Grades.Queries.GetGradeRecordsByCourse;

/// <summary>
/// Handler que obtiene calificaciones de todos los alumnos de un curso en una materia específica
/// </summary>
public class GetGradeRecordsByCourseQueryHandler : IRequestHandler<GetGradeRecordsByCourseQuery, List<GradeRecordDto>>
{
    private readonly IApplicationDbContext _context;

    public GetGradeRecordsByCourseQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GradeRecordDto>> Handle(GetGradeRecordsByCourseQuery request, CancellationToken cancellationToken)
    {
        var gradeRecords = await _context.GradeRecords
            .Where(g => g.CourseSubject!.CourseId == request.CourseId &&
                       g.CourseSubject.SubjectId == request.SubjectId &&
                       g.TermId == request.TermId)
            .Include(g => g.Student)
                .ThenInclude(s => s.User!)
                    .ThenInclude(u => u.Profile)
            .Include(g => g.CourseSubject)
                .ThenInclude(cs => cs.Subject)
            .Include(g => g.CourseSubject)
                .ThenInclude(cs => cs.Course)
            .Include(g => g.Term)
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

        return gradeRecords.OrderBy(g => g.StudentName).ToList();
    }
}

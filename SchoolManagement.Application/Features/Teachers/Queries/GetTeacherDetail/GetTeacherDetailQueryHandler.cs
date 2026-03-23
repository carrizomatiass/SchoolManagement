using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Teachers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Queries.GetTeacherDetail
{
    /// <summary>
    /// Handler que obtiene el detalle completo de un profesor
    /// Incluye todas sus asignaciones con información de cursos y materias
    /// </summary>
    public class GetTeacherDetailQueryHandler : IRequestHandler<GetTeacherDetailQuery, TeacherDetailDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetTeacherDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherDetailDto?> Handle(GetTeacherDetailQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers
                .Include(t => t.User)
                    .ThenInclude(u => u.Profile)
                .Include(t => t.TeacherAssignments)
                    .ThenInclude(ta => ta.CourseSubject)
                        .ThenInclude(cs => cs.Course)
                            .ThenInclude(c => c.Grade)
                .Include(t => t.TeacherAssignments)
                    .ThenInclude(ta => ta.CourseSubject)
                        .ThenInclude(cs => cs.Course)
                            .ThenInclude(c => c.Section)
                .Include(t => t.TeacherAssignments)
                    .ThenInclude(ta => ta.CourseSubject)
                        .ThenInclude(cs => cs.Subject)
                .Include(t => t.TeacherAssignments)
                    .ThenInclude(ta => ta.AcademicYear)
                .Where(t => t.Id == request.TeacherId && !t.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (teacher == null)
                return null;

            var result = new TeacherDetailDto
            {
                Id = teacher.Id,
                UserId = teacher.UserId,
                Email = teacher.User.Email,
                FirstName = teacher.User.Profile!.FirstName,
                LastName = teacher.User.Profile.LastName,
                FullName = teacher.User.Profile.FullName,
                DocumentNumber = teacher.User.Profile.DocumentNumber,
                Phone = teacher.User.Profile.Phone,
                Address = teacher.User.Profile.Address,
                Specialty = teacher.Specialty,
                HireDate = teacher.HireDate,
                IsActive = teacher.IsActive,
                CreatedAt = teacher.CreatedAt,
                Assignments = teacher.TeacherAssignments
                    .Select(ta => new TeacherAssignmentInfo
                    {
                        AssignmentId = ta.Id,
                        CourseName = ta.CourseSubject.Course.Name,
                        SubjectName = ta.CourseSubject.Subject.Name,
                        Year = ta.AcademicYear.Year,
                        GradeName = ta.CourseSubject.Course.Grade.Name,
                        SectionName = ta.CourseSubject.Course.Section.Name
                    })
                    .ToList()
            };

            return result;
        }
    }
}

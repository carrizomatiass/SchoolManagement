using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Queries.GetCourseDetail
{
    /// <summary>
    /// Handler que obtiene el detalle completo de un curso
    /// Incluye toda la información relacionada: año, grado, sección, materias, alumnos
    /// </summary>
    public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, CourseDetailDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCourseDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDetailDto?> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .Include(c => c.AcademicYear)
                .Include(c => c.Grade)
                .Include(c => c.Section)
                .Include(c => c.CourseSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Include(c => c.Enrollments)
                .Where(c => c.Id == request.CourseId && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (course == null)
                return null;

            // Contar alumnos activos matriculados
            var enrolledCount = course.Enrollments
                .Count(e => e.Status == Domain.Enums.EnrollmentStatus.Active);

            var result = new CourseDetailDto
            {
                Id = course.Id,
                Name = course.Name,
                Capacity = course.Capacity,
                EnrolledStudents = enrolledCount,
                CreatedAt = course.CreatedAt,
                AcademicYear = new AcademicYearInfo
                {
                    Id = course.AcademicYear.Id,
                    Year = course.AcademicYear.Year,
                    IsActive = course.AcademicYear.IsActive
                },
                Grade = new GradeInfo
                {
                    Id = course.Grade.Id,
                    GradeNumber = course.Grade.GradeNumber,
                    Name = course.Grade.Name
                },
                Section = new SectionInfo
                {
                    Id = course.Section.Id,
                    Name = course.Section.Name
                },
                Subjects = course.CourseSubjects
                    .Select(cs => new SubjectInfo
                    {
                        Id = cs.Subject.Id,
                        Name = cs.Subject.Name,
                        Description = cs.Subject.Description
                    })
                    .ToList()
            };

            return result;
        }
    }
}

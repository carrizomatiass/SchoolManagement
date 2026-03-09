using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Queries.GetCourses
{
    /// <summary>
    /// Handler que obtiene la lista de cursos
    /// Permite filtrar por año académico y/o grado
    /// Ordena por grado y luego por sección
    /// </summary>
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCoursesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Courses
                .Include(c => c.Grade)
                .Include(c => c.Section)
                .AsQueryable();

            // Filtrar por año académico si se especifica
            if (request.AcademicYearId.HasValue)
            {
                query = query.Where(c => c.AcademicYearId == request.AcademicYearId.Value);
            }

            // Filtrar por grado si se especifica
            if (request.GradeId.HasValue)
            {
                query = query.Where(c => c.GradeId == request.GradeId.Value);
            }

            // Ordenar por grado y luego por sección
            query = query
                .OrderBy(c => c.Grade.GradeNumber)
                .ThenBy(c => c.Section.Name);

            var courses = await query
                .Where(c => !c.IsDeleted)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    AcademicYearId = c.AcademicYearId,
                    GradeId = c.GradeId,
                    SectionId = c.SectionId,
                    Name = c.Name,
                    Capacity = c.Capacity,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return courses;
        }
       }
    }

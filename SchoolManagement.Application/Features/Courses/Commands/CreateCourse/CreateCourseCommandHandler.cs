using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Commands.CreateCourse
{
    /// <summary>
    /// Handler que procesa la creación de un nuevo curso
    /// Verifica que no exista un curso duplicado (mismo grado + sección + año)
    /// Genera el nombre automáticamente si no se proporciona
    /// </summary>
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateCourseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            // Verificar que existan el año académico, grado y sección
            var academicYear = await _context.AcademicYears
                .FirstOrDefaultAsync(a => a.Id == request.AcademicYearId, cancellationToken);

            if (academicYear == null)
                throw new InvalidOperationException("El año académico no existe");

            var grade = await _context.Grades
                .FirstOrDefaultAsync(g => g.Id == request.GradeId, cancellationToken);

            if (grade == null)
                throw new InvalidOperationException("El grado no existe");

            var section = await _context.Sections
                .FirstOrDefaultAsync(s => s.Id == request.SectionId, cancellationToken);

            if (section == null)
                throw new InvalidOperationException("La sección no existe");

            // Verificar si ya existe un curso con la misma combinación
            var existingCourse = await _context.Courses
                .FirstOrDefaultAsync(c =>
                    c.AcademicYearId == request.AcademicYearId &&
                    c.GradeId == request.GradeId &&
                    c.SectionId == request.SectionId,
                    cancellationToken);

            if (existingCourse != null)
                throw new InvalidOperationException(
                    $"Ya existe un curso para {grade.Name} sección {section.Name} del año {academicYear.Year}");

            // Generar nombre automáticamente si no se proporciona
            var courseName = request.Name ?? $"{grade.GradeNumber}° {section.Name} {academicYear.Year}";

            // Crear el nuevo curso
            var course = new Course
            {
                Id = Guid.NewGuid(),
                AcademicYearId = request.AcademicYearId,
                GradeId = request.GradeId,
                SectionId = request.SectionId,
                Name = courseName,
                Capacity = request.Capacity
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync(cancellationToken);

            return course.Id;
        }
    }
}

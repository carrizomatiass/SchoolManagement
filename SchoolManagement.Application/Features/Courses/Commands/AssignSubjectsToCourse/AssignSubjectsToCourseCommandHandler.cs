using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Commands.AssignSubjectsToCourse
{
    /// <summary>
    /// Handler que asigna materias a un curso
    /// No permite duplicados - si una materia ya está asignada, la omite
    /// </summary>
    public class AssignSubjectsToCourseCommandHandler : IRequestHandler<AssignSubjectsToCourseCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public AssignSubjectsToCourseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AssignSubjectsToCourseCommand request, CancellationToken cancellationToken)
        {
            // Verificar que el curso exista
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

            if (course == null)
                throw new InvalidOperationException("El curso no existe");

            // Obtener las materias ya asignadas a este curso
            var existingAssignments = await _context.CourseSubjects
                .Where(cs => cs.CourseId == request.CourseId)
                .Select(cs => cs.SubjectId)
                .ToListAsync(cancellationToken);

            // Filtrar solo las materias nuevas (no duplicadas)
            var newSubjectIds = request.SubjectIds
                .Where(id => !existingAssignments.Contains(id))
                .ToList();

            if (newSubjectIds.Count == 0)
                throw new InvalidOperationException("Todas las materias ya están asignadas a este curso");

            // Verificar que todas las materias existan
            var subjects = await _context.Subjects
                .Where(s => newSubjectIds.Contains(s.Id))
                .ToListAsync(cancellationToken);

            if (subjects.Count != newSubjectIds.Count)
                throw new InvalidOperationException("Una o más materias no existen");

            // Crear las asignaciones
            foreach (var subjectId in newSubjectIds)
            {
                var courseSubject = new CourseSubject
                {
                    Id = Guid.NewGuid(),
                    CourseId = request.CourseId,
                    SubjectId = subjectId
                };

                _context.CourseSubjects.Add(courseSubject);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

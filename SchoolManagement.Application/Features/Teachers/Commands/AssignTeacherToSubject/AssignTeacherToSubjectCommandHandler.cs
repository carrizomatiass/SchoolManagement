using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Commands.AssignTeacherToSubject
{
    /// <summary>
    /// Handler que asigna un profesor a una materia de un curso
    /// Verifica que existan el profesor, curso y materia
    /// Verifica que la materia esté asignada al curso
    /// No permite asignaciones duplicadas
    /// </summary>
    public class AssignTeacherToSubjectCommandHandler : IRequestHandler<AssignTeacherToSubjectCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public AssignTeacherToSubjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AssignTeacherToSubjectCommand request, CancellationToken cancellationToken)
        {
            // Verificar que el profesor exista y esté activo
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == request.TeacherId && t.IsActive, cancellationToken);

            if (teacher == null)
                throw new InvalidOperationException("El profesor no existe o no está activo");

            // Verificar que el curso exista
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

            if (course == null)
                throw new InvalidOperationException("El curso no existe");

            // Verificar que la materia esté asignada a ese curso
            var courseSubject = await _context.CourseSubjects
                .FirstOrDefaultAsync(cs =>
                    cs.CourseId == request.CourseId &&
                    cs.SubjectId == request.SubjectId,
                    cancellationToken);

            if (courseSubject == null)
                throw new InvalidOperationException("La materia no está asignada a ese curso");

            // Verificar que no exista ya una asignación igual
            var existingAssignment = await _context.TeacherAssignments
                .FirstOrDefaultAsync(ta =>
                    ta.TeacherId == request.TeacherId &&
                    ta.CourseSubjectId == courseSubject.Id &&
                    ta.AcademicYearId == request.AcademicYearId,
                    cancellationToken);

            if (existingAssignment != null)
                throw new InvalidOperationException("El profesor ya está asignado a esta materia en este curso");

            // Crear la asignación
            var assignment = new TeacherAssignment
            {
                Id = Guid.NewGuid(),
                TeacherId = request.TeacherId,
                CourseSubjectId = courseSubject.Id,
                AcademicYearId = request.AcademicYearId
            };

            _context.TeacherAssignments.Add(assignment);
            await _context.SaveChangesAsync(cancellationToken);

            return assignment.Id;
        }
    }
}

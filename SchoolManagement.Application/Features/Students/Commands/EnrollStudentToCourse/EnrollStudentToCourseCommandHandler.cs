using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Commands.EnrollStudentToCourse
{
    /// <summary>
    /// Handler que matricula un alumno a un curso
    /// Verifica capacidad del curso y que el alumno no esté ya matriculado
    /// </summary>
    public class EnrollStudentToCourseCommandHandler : IRequestHandler<EnrollStudentToCourseCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public EnrollStudentToCourseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(EnrollStudentToCourseCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == request.StudentId && s.IsActive, cancellationToken);

            if (student == null)
                throw new InvalidOperationException("El alumno no existe o no está activo");

            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

            if (course == null)
                throw new InvalidOperationException("El curso no existe");

            var activeEnrollments = course.Enrollments.Count(e => e.Status == EnrollmentStatus.Active);

            if (activeEnrollments >= course.Capacity)
                throw new InvalidOperationException("El curso ha alcanzado su capacidad máxima");

            var existingEnrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e =>
                    e.StudentId == request.StudentId &&
                    e.CourseId == request.CourseId &&
                    e.Status == EnrollmentStatus.Active,
                    cancellationToken);

            if (existingEnrollment != null)
                throw new InvalidOperationException("El alumno ya está matriculado en este curso");

            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                EnrollmentDate = request.EnrollmentDate,
                Status = EnrollmentStatus.Active
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync(cancellationToken);

            return enrollment.Id;
        }
    }
}

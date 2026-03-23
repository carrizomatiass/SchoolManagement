using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Commands.AssignParentToStudent
{
    /// <summary>
    /// Handler que asocia un padre/tutor con un alumno
    /// Verifica que no exista ya la relación
    /// </summary>
    public class AssignParentToStudentCommandHandler : IRequestHandler<AssignParentToStudentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public AssignParentToStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AssignParentToStudentCommand request, CancellationToken cancellationToken)
        {
            var parent = await _context.Parents
                .FirstOrDefaultAsync(p => p.Id == request.ParentId, cancellationToken);

            if (parent == null)
                throw new InvalidOperationException("El padre/tutor no existe");

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

            if (student == null)
                throw new InvalidOperationException("El alumno no existe");

            var existingRelation = await _context.StudentParents
                .FirstOrDefaultAsync(sp =>
                    sp.ParentId == request.ParentId &&
                    sp.StudentId == request.StudentId,
                    cancellationToken);

            if (existingRelation != null)
                throw new InvalidOperationException("El padre/tutor ya está asociado a este alumno");

            var studentParent = new StudentParent
            {
                Id = Guid.NewGuid(),
                ParentId = request.ParentId,
                StudentId = request.StudentId,
                IsPrimaryContact = request.IsPrimaryContact
            };

            _context.StudentParents.Add(studentParent);
            await _context.SaveChangesAsync(cancellationToken);

            return studentParent.Id;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Grades.Commands.CreateGrade
{
    /// <summary>
    /// Handler que procesa la creación de un nuevo grado
    /// Verifica que no exista un grado duplicado antes de crear
    /// </summary>
    public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateGradeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            // Verificar si ya existe un grado con ese número y nivel
            var existingGrade = await _context.Grades
                .FirstOrDefaultAsync(g => g.GradeNumber == request.GradeNumber &&
                                         g.Level == request.Level,
                                    cancellationToken);

            if (existingGrade != null)
                throw new InvalidOperationException($"Ya existe un grado {request.GradeNumber}° en nivel {request.Level}");

            // Crear el nuevo grado
            var grade = new Grade
            {
                Id = Guid.NewGuid(),
                Level = request.Level,
                GradeNumber = request.GradeNumber,
                Name = request.Name
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync(cancellationToken);

            return grade.Id;
        }
    }
}

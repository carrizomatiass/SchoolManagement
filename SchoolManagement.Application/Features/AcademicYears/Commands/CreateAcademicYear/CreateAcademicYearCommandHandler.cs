using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AcademicYears.Commands.CreateAcademicYear
{
    /// <summary>
    /// Handler que procesa la creación de un nuevo año académico
    /// Si el año se marca como activo, desactiva los demás años existentes
    /// </summary>
    public class CreateAcademicYearCommandHandler : IRequestHandler<CreateAcademicYearCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateAcademicYearCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAcademicYearCommand request, CancellationToken cancellationToken)
        {
            // Verificar si ya existe un año académico con ese año
            var existingYear = await _context.AcademicYears
                .FirstOrDefaultAsync(a => a.Year == request.Year, cancellationToken);

            if (existingYear != null)
                throw new InvalidOperationException($"Ya existe un año académico para el año {request.Year}");

            if (request.IsActive)
            {
                var activeYears = await _context.AcademicYears
                    .Where(a => a.IsActive)
                    .ToListAsync(cancellationToken);

                foreach (var year in activeYears)
                {
                    year.IsActive = false;
                }
            }

            // Crear el nuevo año académico
            var academicYear = new AcademicYear
            {
                Id = Guid.NewGuid(),
                Year = request.Year,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = request.IsActive
            };

            _context.AcademicYears.Add(academicYear);
            await _context.SaveChangesAsync(cancellationToken);

            return academicYear.Id;
        }
    }
}

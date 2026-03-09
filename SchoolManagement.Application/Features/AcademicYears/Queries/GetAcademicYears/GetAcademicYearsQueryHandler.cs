using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.AcademicYears.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AcademicYears.Queries.GetAcademicYears
{
    /// <summary>
    /// Handler que obtiene la lista de años académicos
    /// Puede filtrar por activos o traer todos
    /// </summary>
    public class GetAcademicYearsQueryHandler : IRequestHandler<GetAcademicYearsQuery, List<AcademicYearDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAcademicYearsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicYearDto>> Handle(GetAcademicYearsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.AcademicYears.AsQueryable();

            // Filtrar por activos si se especifica
            if (request.OnlyActive.HasValue && request.OnlyActive.Value)
            {
                query = query.Where(a => a.IsActive);
            }

            // Ordenar por año descendente (más recientes primero)
            query = query.OrderByDescending(a => a.Year);

            var academicYears = await query
                .Where(a => !a.IsDeleted)
                .Select(a => new AcademicYearDto
                {
                    Id = a.Id,
                    Year = a.Year,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return academicYears;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Grades.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Grades.Queries.GetGrades
{
    /// <summary>
    /// Handler que obtiene la lista de grados
    /// Puede filtrar por nivel educativo (primaria o secundaria)
    /// Ordena por número de grado
    /// </summary>
    public class GetGradesQueryHandler : IRequestHandler<GetGradesQuery, List<GradeDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetGradesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GradeDto>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Grades.AsQueryable();

            // Filtrar por nivel si se especifica
            if (!string.IsNullOrEmpty(request.Level))
            {
                query = query.Where(g => g.Level == request.Level);
            }

            // Ordenar por número de grado (1°, 2°, 3°, etc.)
            query = query.OrderBy(g => g.GradeNumber);

            var grades = await query
                .Where(g => !g.IsDeleted)
                .Select(g => new GradeDto
                {
                    Id = g.Id,
                    Level = g.Level,
                    GradeNumber = g.GradeNumber,
                    Name = g.Name,
                    CreatedAt = g.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return grades;
        }
    }

}

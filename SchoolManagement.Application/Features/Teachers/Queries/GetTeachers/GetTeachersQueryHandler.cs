using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Teachers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Queries.GetTeachers
{
    /// <summary>
    /// Handler que obtiene la lista de profesores
    /// Incluye información del usuario y perfil
    /// Ordena alfabéticamente por apellido
    /// </summary>
    public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, List<TeacherDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTeachersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherDto>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Teachers
                .Include(t => t.User)
                    .ThenInclude(u => u.Profile)
                .AsQueryable();

            // Filtrar por activos si se especifica
            if (request.OnlyActive.HasValue && request.OnlyActive.Value)
            {
                query = query.Where(t => t.IsActive);
            }

            var teachers = await query
                .Where(t => !t.IsDeleted)
                .OrderBy(t => t.User.Profile!.LastName)
                .ThenBy(t => t.User.Profile!.FirstName)
                .Select(t => new TeacherDto
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    Email = t.User.Email,
                    FirstName = t.User.Profile!.FirstName,
                    LastName = t.User.Profile!.LastName,
                    FullName = t.User.Profile!.FullName,
                    DocumentNumber = t.User.Profile.DocumentNumber,
                    Phone = t.User.Profile.Phone,
                    Specialty = t.Specialty,
                    HireDate = t.HireDate,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return teachers;
        }
    }
}

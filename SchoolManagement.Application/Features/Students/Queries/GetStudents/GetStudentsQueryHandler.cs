using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Queries.GetStudents
{
    /// <summary>
    /// Handler que obtiene lista de alumnos
    /// Puede filtrar por curso específico
    /// </summary>
    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<Models.StudentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Students
                .Include(s => s.User)
                    .ThenInclude(u => u.Profile)
                .AsQueryable();

            if (request.OnlyActive.HasValue && request.OnlyActive.Value)
            {
                query = query.Where(s => s.IsActive);
            }

            if (request.CourseId.HasValue)
            {
                query = query.Where(s => s.Enrollments
                    .Any(e => e.CourseId == request.CourseId.Value &&
                             e.Status == Domain.Enums.EnrollmentStatus.Active));
            }

            var students = await query
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.User.Profile!.LastName)
                .ThenBy(s => s.User.Profile!.FirstName)
                .Select(s => new Models.StudentDto
                {
                    Id = s.Id,
                    Email = s.User.Email,
                    FirstName = s.User.Profile!.FirstName,
                    LastName = s.User.Profile!.LastName,
                    FullName = s.User.Profile!.FullName,
                    EnrollmentNumber = s.EnrollmentNumber,
                    DateOfBirth = s.DateOfBirth,
                    Gender = s.Gender,
                    IsActive = s.IsActive
                })
                .ToListAsync(cancellationToken);

            return students;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Subjects.Queries.GetSubjects
{

    /// <summary>
    /// Handler que obtiene la lista de materias ordenadas alfabéticamente
    /// </summary>
    public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, List<Models.SubjectDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetSubjectsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.SubjectDto>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _context.Subjects
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.Name)
                .Select(s => new Models.SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return subjects;
        }
    }
}

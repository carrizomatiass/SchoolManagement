using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;    
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Queries.GetParents
{
    /// <summary>
    /// Handler que obtiene lista de padres/tutores
    /// </summary>
    public class GetParentsQueryHandler : IRequestHandler<GetParentsQuery, List<Models.ParentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetParentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.ParentDto>> Handle(GetParentsQuery request, CancellationToken cancellationToken)
        {
            var parents = await _context.Parents
                .Include(p => p.User)
                    .ThenInclude(u => u.Profile)
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.User.Profile!.LastName)
                .ThenBy(p => p.User.Profile!.FirstName)
                .Select(p => new Models.ParentDto
                {
                    Id = p.Id,
                    Email = p.User.Email,
                    FirstName = p.User.Profile!.FirstName,
                    LastName = p.User.Profile!.LastName,
                    FullName = p.User.Profile!.FullName,
                    Relationship = p.Relationship,
                    Phone = p.User.Profile.Phone
                })
                .ToListAsync(cancellationToken);

            return parents;
        }
    }
}

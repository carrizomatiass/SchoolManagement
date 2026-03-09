using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Sections.Models;

namespace SchoolManagement.Application.Features.Sections.Queries.GetSections;

/// <summary>
/// Query para obtener todas las secciones
/// </summary>
public class GetSectionsQuery : IRequest<List<SectionDto>>
{
}

/// <summary>
/// Handler que obtiene la lista de secciones ordenadas alfabéticamente
/// </summary>
public class GetSectionsQueryHandler : IRequestHandler<GetSectionsQuery, List<SectionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSectionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SectionDto>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
    {
        var sections = await _context.Sections
            .Where(s => !s.IsDeleted)
            .OrderBy(s => s.Name)
            .Select(s => new SectionDto
            {
                Id = s.Id,
                Name = s.Name,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return sections;
    }
}

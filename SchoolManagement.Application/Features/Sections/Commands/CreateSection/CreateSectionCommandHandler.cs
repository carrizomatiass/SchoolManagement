using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Sections.Commands.CreateSection;

/// <summary>
/// Handler que procesa la creación de una nueva sección
/// Verifica que no exista una sección duplicada antes de crear
/// </summary>
public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSectionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        // Verificar si ya existe una sección con ese nombre
        var existingSection = await _context.Sections
            .FirstOrDefaultAsync(s => s.Name == request.Name, cancellationToken);

        if (existingSection != null)
            throw new InvalidOperationException($"Ya existe una sección con el nombre {request.Name}");

        // Crear la nueva sección
        var section = new Section
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        _context.Sections.Add(section);
        await _context.SaveChangesAsync(cancellationToken);

        return section.Id;
    }
}

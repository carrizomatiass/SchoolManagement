using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Subjects.Commands.CreateSubject;

/// <summary>
/// Handler que procesa la creación de una nueva materia
/// Verifica que no exista una materia duplicada antes de crear
/// </summary>
public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSubjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        // Verificar si ya existe una materia con ese nombre
        var existingSubject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Name == request.Name, cancellationToken);

        if (existingSubject != null)
            throw new InvalidOperationException($"Ya existe una materia con el nombre {request.Name}");

        // Crear la nueva materia
        var subject = new Subject
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync(cancellationToken);

        return subject.Id;
    }
}

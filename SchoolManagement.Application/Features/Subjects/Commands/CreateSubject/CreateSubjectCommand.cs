using MediatR;

namespace SchoolManagement.Application.Features.Subjects.Commands.CreateSubject;

/// <summary>
/// Comando para crear una nueva materia
/// Ejemplo: Matemática, Lengua, Ciencias Naturales, etc.
/// </summary>
public class CreateSubjectCommand : IRequest<Guid>
{
    /// <summary>
    /// Nombre de la materia (ejemplo: "Matemática", "Lengua")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción opcional de la materia
    /// </summary>
    public string? Description { get; set; }
}

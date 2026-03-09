using MediatR;

namespace SchoolManagement.Application.Features.Sections.Commands.CreateSection;

/// <summary>
/// Comando para crear una nueva sección
/// Ejemplo: Sección A, Sección B, Sección C
/// Las secciones se usan para dividir los cursos dentro de un mismo grado
/// </summary>
public class CreateSectionCommand : IRequest<Guid>
{
    /// <summary>
    /// Nombre de la sección (generalmente una letra: A, B, C, etc.)
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

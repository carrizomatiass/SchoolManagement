namespace SchoolManagement.Application.Features.Sections.Models;

/// <summary>
/// DTO para sección
/// Representa una división dentro de un grado (A, B, C, etc.)
/// </summary>
public class SectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

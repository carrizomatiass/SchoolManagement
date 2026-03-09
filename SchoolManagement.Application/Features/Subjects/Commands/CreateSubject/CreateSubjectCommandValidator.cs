using FluentValidation;

namespace SchoolManagement.Application.Features.Subjects.Commands.CreateSubject;

/// <summary>
/// Validador para comando de crear materia
/// </summary>
public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la materia es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres");
    }
}

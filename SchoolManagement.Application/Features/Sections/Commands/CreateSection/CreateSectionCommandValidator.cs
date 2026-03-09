using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Sections.Commands.CreateSection
{
    /// <summary>
    /// Validador para el comando de crear sección
    /// </summary>
    public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sección es requerido")
                .MaximumLength(10).WithMessage("El nombre no puede exceder 10 caracteres");
        }
    }
}

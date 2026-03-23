using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Commands.CreateTeacher
{
    /// <summary>
    /// Validador para el comando de crear profesor
    /// </summary>
    public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido")
                .EmailAddress().WithMessage("El email no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido")
                .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres");

            RuleFor(x => x.DocumentNumber)
                .MaximumLength(50).WithMessage("El número de documento no puede exceder 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.DocumentNumber));

            RuleFor(x => x.Phone)
                .MaximumLength(50).WithMessage("El teléfono no puede exceder 50 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Specialty)
                .MaximumLength(200).WithMessage("La especialidad no puede exceder 200 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Specialty));
        }
    }

}

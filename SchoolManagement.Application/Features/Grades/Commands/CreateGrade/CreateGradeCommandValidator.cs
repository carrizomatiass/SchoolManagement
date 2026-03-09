using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Grades.Commands.CreateGrade
{
    /// <summary>
    /// Validador para el comando de crear grado
    /// Asegura que los datos sean válidos antes de crear el grado
    /// </summary>
    public class CreateGradeCommandValidator : AbstractValidator<CreateGradeCommand>
    {
        public CreateGradeCommandValidator()
        {
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("El nivel es requerido")
                .Must(level => level == "primary" || level == "secondary")
                .WithMessage("El nivel debe ser 'primary' o 'secondary'");

            RuleFor(x => x.GradeNumber)
                .GreaterThan(0).WithMessage("El número de grado debe ser mayor a 0")
                .LessThanOrEqualTo(6).WithMessage("El número de grado no puede ser mayor a 6");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");
        }
    }
}

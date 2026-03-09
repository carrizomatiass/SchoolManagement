using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AcademicYears.Commands.CreateAcademicYear
{
    /// <summary>
    /// Validador para el comando de crear año academico
    /// Valida que los datos sean correctos antes de procesar
    /// </summary>
    public class CreateAcademicYearCommandValidator : AbstractValidator<CreateAcademicYearCommand>
    {
        public CreateAcademicYearCommandValidator()
        {
            RuleFor(x => x.Year)
                .GreaterThan(2000).WithMessage("El año debe ser mayor a 2000")
                .LessThan(2100).WithMessage("El año debe ser menor a 2100");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("La fecha de inicio es requerida")
                .LessThan(x => x.EndDate).WithMessage("La fecha de inicio debe ser anterior a la fecha de fin");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("La fecha de fin es requerida")
                .GreaterThan(x => x.StartDate).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio");
        }
    }
}

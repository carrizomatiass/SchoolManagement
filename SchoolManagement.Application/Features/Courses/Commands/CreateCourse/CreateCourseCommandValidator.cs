using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Commands.CreateCourse
{
    /// <summary>
    /// Validador para el comando de crear curso
    /// Valida que todos los datos requeridos estén presentes
    /// </summary>
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.AcademicYearId)
                .NotEmpty().WithMessage("El año académico es requerido");

            RuleFor(x => x.GradeId)
                .NotEmpty().WithMessage("El grado es requerido");

            RuleFor(x => x.SectionId)
                .NotEmpty().WithMessage("La sección es requerida");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0")
                .LessThanOrEqualTo(50).WithMessage("La capacidad no puede exceder 50 alumnos");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Name));
        }
    }
}

using FluentValidation;

namespace SchoolManagement.Application.Features.Attendances.Commands.CreateAttendance;

/// <summary>
/// Validador para el comando de crear asistencia
/// </summary>
public class CreateAttendanceCommandValidator : AbstractValidator<CreateAttendanceCommand>
{
    public CreateAttendanceCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("El ID del alumno es requerido");

        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("El ID del curso es requerido");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("La fecha es requerida")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("La fecha no puede ser futura");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("El estado de asistencia es requerido")
            .InclusiveBetween(1, 4)
            .WithMessage("El estado debe ser un valor válido");

        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .WithMessage("El ID del profesor es requerido");
    }
}

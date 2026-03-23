using FluentValidation;

namespace SchoolManagement.Application.Features.Grades.Commands.CreateGradeRecord;

/// <summary>
/// Validador para el comando de crear calificación
/// </summary>
public class CreateGradeRecordCommandValidator : AbstractValidator<CreateGradeRecordCommand>
{
    public CreateGradeRecordCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("El ID del alumno es requerido");

        RuleFor(x => x.CourseSubjectId)
            .NotEmpty()
            .WithMessage("El ID de la materia es requerido");

        RuleFor(x => x.TermId)
            .NotEmpty()
            .WithMessage("El ID del trimestre es requerido");

        RuleFor(x => x.GradeValue)
            .InclusiveBetween(1m, 10m)
            .WithMessage("La calificación debe estar entre 1 y 10");

        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .WithMessage("El ID del profesor es requerido");
    }
}

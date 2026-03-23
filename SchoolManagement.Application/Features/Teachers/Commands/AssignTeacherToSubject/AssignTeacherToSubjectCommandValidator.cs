using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Commands.AssignTeacherToSubject
{
    /// <summary>
    /// Validador para asignar profesor a materia
    /// </summary>
    public class AssignTeacherToSubjectCommandValidator : AbstractValidator<AssignTeacherToSubjectCommand>
    {
        public AssignTeacherToSubjectCommandValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("El profesor es requerido");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("El curso es requerido");

            RuleFor(x => x.SubjectId)
                .NotEmpty().WithMessage("La materia es requerida");

            RuleFor(x => x.AcademicYearId)
                .NotEmpty().WithMessage("El año académico es requerido");
        }
    }

}

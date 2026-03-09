using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Commands.AssignSubjectsToCourse
{
    /// <summary>
    /// Validador para asignar materias a curso
    /// </summary>
    public class AssignSubjectsToCourseCommandValidator : AbstractValidator<AssignSubjectsToCourseCommand>
    {
        public AssignSubjectsToCourseCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("El curso es requerido");

            RuleFor(x => x.SubjectIds)
                .NotEmpty().WithMessage("Debe proporcionar al menos una materia")
                .Must(list => list.Count > 0).WithMessage("Debe proporcionar al menos una materia");
        }
    }
}

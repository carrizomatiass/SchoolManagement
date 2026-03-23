using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Commands.EnrollStudentToCourse
{
    public class EnrollStudentToCourseCommandValidator : AbstractValidator<EnrollStudentToCourseCommand>
    {
        public EnrollStudentToCourseCommandValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("El alumno es requerido");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("El curso es requerido");

            RuleFor(x => x.EnrollmentDate)
                .NotEmpty().WithMessage("La fecha de matrícula es requerida");
        }
    }
}

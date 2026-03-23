using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Commands.AssignParentToStudent
{
    public class AssignParentToStudentCommandValidator : AbstractValidator<AssignParentToStudentCommand>
    {
        public AssignParentToStudentCommandValidator()
        {
            RuleFor(x => x.ParentId)
                .NotEmpty().WithMessage("El padre/tutor es requerido");

            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("El alumno es requerido");
        }
    }
}

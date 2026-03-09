using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Commands.AssignSubjectsToCourse
{
    /// <summary>
    /// Comando para asignar materias a un curso
    /// Permite asignar múltiples materias de una vez
    /// </summary>
    public class AssignSubjectsToCourseCommand : IRequest<bool>
    {
        /// <summary>
        /// ID del curso al que se asignarán las materias
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Lista de IDs de las materias a asignar
        /// </summary>
        public List<Guid> SubjectIds { get; set; } = new();
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Commands.AssignTeacherToSubject
{
    /// <summary>
    /// Comando para asignar un profesor a una materia de un curso específico
    /// Ejemplo: Asignar al profesor Juan García para dar Matemática en 1° A 2024
    /// </summary>
    public class AssignTeacherToSubjectCommand : IRequest<Guid>
    {
        /// <summary>
        /// ID del profesor
        /// </summary>
        public Guid TeacherId { get; set; }

        /// <summary>
        /// ID del curso
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// ID de la materia
        /// </summary>
        public Guid SubjectId { get; set; }

        /// <summary>
        /// ID del año académico
        /// </summary>
        public Guid AcademicYearId { get; set; }
    }
}

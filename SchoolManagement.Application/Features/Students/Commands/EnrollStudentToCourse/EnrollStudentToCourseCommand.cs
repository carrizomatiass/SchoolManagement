using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Commands.EnrollStudentToCourse
{
    /// <summary>
    /// Comando para matricular un alumno a un curso
    /// Ejemplo: Matricular a Juan Pérez en 1° A 2024
    /// </summary>
    public class EnrollStudentToCourseCommand : IRequest<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.Features.Courses.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Queries.GetCourseDetail
{
    /// <summary>
    /// Query para obtener el detalle completo de un curso
    /// Incluye año académico, grado, sección, materias y cantidad de alumnos
    /// </summary>
    public class GetCourseDetailQuery : IRequest<CourseDetailDto?>
    {
        /// <summary>
        /// ID del curso a consultar
        /// </summary>
        public Guid CourseId { get; set; }
    }
}

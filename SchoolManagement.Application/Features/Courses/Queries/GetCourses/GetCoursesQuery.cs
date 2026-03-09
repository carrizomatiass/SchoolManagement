using MediatR;
using SchoolManagement.Application.Features.Courses.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Queries.GetCourses
{
    /// <summary>
    /// Query para obtener lista de cursos
    /// Permite filtrar por año académico y/o grado
    /// </summary>
    public class GetCoursesQuery : IRequest<List<CourseDto>>
    {
        /// <summary>
        /// Filtro opcional: ID del año académico
        /// Si se proporciona, solo trae cursos de ese año
        /// </summary>
        public Guid? AcademicYearId { get; set; }

        /// <summary>
        /// Filtro opcional: ID del grado
        /// Si se proporciona, solo trae cursos de ese grado
        /// </summary>
        public Guid? GradeId { get; set; }
    }
}

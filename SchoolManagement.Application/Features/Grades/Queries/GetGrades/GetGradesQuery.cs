using MediatR;
using SchoolManagement.Application.Features.Grades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Grades.Queries.GetGrades
{
    /// <summary>
    /// Query para obtener todos los grados
    /// Puede filtrar por nivel (primaria o secundaria)
    /// </summary>
    public class GetGradesQuery : IRequest<List<GradeDto>>
    {
        /// <summary>
        /// Filtro opcional por nivel: "primary" o "secondary"
        /// Si es null, trae todos los grados
        /// </summary>
        public string? Level { get; set; }
    }
}

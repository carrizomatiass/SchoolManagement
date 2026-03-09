using MediatR;
using SchoolManagement.Application.Features.AcademicYears.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AcademicYears.Queries.GetAcademicYears
{
    /// <summary>
    /// Query para obtener todos los años académicos
    /// Permite filtrar por activo o traer todos
    /// </summary>
    public class GetAcademicYearsQuery : IRequest<List<AcademicYearDto>>
    {
        /// <summary>
        /// Si es true, solo trae los años académicos activos
        /// Si es false o null, trae todos
        /// </summary>
        public bool? OnlyActive { get; set; }
    }
}

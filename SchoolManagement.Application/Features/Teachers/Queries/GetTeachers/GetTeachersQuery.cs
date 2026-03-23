using MediatR;
using SchoolManagement.Application.Features.Teachers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Queries.GetTeachers
{
    /// <summary>
    /// Query para obtener lista de profesores
    /// Permite filtrar por activos o traer todos
    /// </summary>
    public class GetTeachersQuery : IRequest<List<TeacherDto>>
    {
        /// <summary>
        /// Si es true, solo trae profesores activos
        /// Si es false o null, trae todos
        /// </summary>
        public bool? OnlyActive { get; set; }
    }
}

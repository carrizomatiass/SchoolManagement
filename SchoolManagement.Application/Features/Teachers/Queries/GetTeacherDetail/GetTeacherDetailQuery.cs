using MediatR;
using SchoolManagement.Application.Features.Teachers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Queries.GetTeacherDetail
{
    /// <summary>
    /// Query para obtener el detalle completo de un profesor
    /// Incluye todas sus asignaciones a cursos y materias
    /// </summary>
    public class GetTeacherDetailQuery : IRequest<TeacherDetailDto?>
    {
        /// <summary>
        /// ID del profesor a consultar
        /// </summary>
        public Guid TeacherId { get; set; }
    }
}

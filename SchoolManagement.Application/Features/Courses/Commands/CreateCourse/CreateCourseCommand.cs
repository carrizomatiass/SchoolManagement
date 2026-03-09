using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Commands.CreateCourse
{
    /// <summary>
    /// Comando para crear un nuevo curso
    /// Un curso es la combinación de: Grado + Sección + Año Académico
    /// Ejemplo: 1° A 2024 (Primer Grado, Sección A, del año 2024)
    /// </summary>
    public class CreateCourseCommand : IRequest<Guid>
    {  
        public Guid AcademicYearId { get; set; }
        public Guid GradeId { get; set; }  
        public Guid SectionId { get; set; }  
        public string? Name { get; set; }
        public int Capacity { get; set; }
    }
}

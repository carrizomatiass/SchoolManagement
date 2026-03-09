using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Grades.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) para grado
    /// Información que se envía al frontend sobre un grado escolar
    /// </summary>
    public class GradeDto
    {
        public Guid Id { get; set; }
        public string Level { get; set; } = string.Empty;
        public int GradeNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

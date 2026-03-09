using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Grades.Commands.CreateGrade
{
    /// <summary>
    /// Comando para crear un nuevo grado escolar
    /// Ejemplo: Primer Grado (1°), Segundo Grado (2°), etc.
    /// </summary>
    public class CreateGradeCommand : IRequest<Guid>
    {
        public string Level { get; set; } = string.Empty;
        public int GradeNumber { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

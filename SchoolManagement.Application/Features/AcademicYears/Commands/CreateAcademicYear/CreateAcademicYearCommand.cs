using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AcademicYears.Commands.CreateAcademicYear
{
    /// <summary>
    /// Comando para crear un nuevo año académico
    /// Ejemplo: Año 2024, del 01/03/2024 al 15/12/2024
    /// </summary>
    public class CreateAcademicYearCommand : IRequest<Guid>
    {
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

}

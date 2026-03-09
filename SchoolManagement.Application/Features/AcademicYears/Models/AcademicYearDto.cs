using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.AcademicYears.Models;

/// <summary>
/// DTO (Data Transfer Object) para año académico
/// Representa la información que se envía al frontend
/// </summary>
public class AcademicYearDto
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

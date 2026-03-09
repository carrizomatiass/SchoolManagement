using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Courses.Models
{
    /// <summary>
    /// DTO básico para curso
    /// Información resumida de un curso
    /// </summary>
    public class CourseDto
    {
        public Guid Id { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid GradeId { get; set; }
        public Guid SectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// DTO detallado para curso
    /// Incluye información completa del año académico, grado, sección y materias
    /// </summary>
    public class CourseDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        /// <summary>
        /// Información del año académico
        /// </summary>
        public AcademicYearInfo AcademicYear { get; set; } = new();

        /// <summary>
        /// Información del grado
        /// </summary>
        public GradeInfo Grade { get; set; } = new();

        /// <summary>
        /// Información de la sección
        /// </summary>
        public SectionInfo Section { get; set; } = new();

        /// <summary>
        /// Materias asignadas a este curso
        /// </summary>
        public List<SubjectInfo> Subjects { get; set; } = new();

        /// <summary>
        /// Cantidad actual de alumnos matriculados
        /// </summary>
        public int EnrolledStudents { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Información resumida del año académico
    /// </summary>
    public class AcademicYearInfo
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Información resumida del grado
    /// </summary>
    public class GradeInfo
    {
        public Guid Id { get; set; }
        public int GradeNumber { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// Información resumida de la sección
    /// </summary>
    public class SectionInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// Información resumida de una materia
    /// </summary>
    public class SubjectInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}

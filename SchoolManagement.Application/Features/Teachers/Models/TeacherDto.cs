using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Models
{
    /// <summary>
    /// DTO básico para profesor
    /// Información resumida de un profesor
    /// </summary>
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }
        public string? Phone { get; set; }
        public string? Specialty { get; set; }
        public DateTime? HireDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// DTO detallado para profesor
    /// Incluye información de asignaciones a cursos y materias
    /// </summary>
    public class TeacherDetailDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Specialty { get; set; }
        public DateTime? HireDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Lista de cursos y materias que dicta el profesor
        /// </summary>
        public List<TeacherAssignmentInfo> Assignments { get; set; } = new();

        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Información de una asignación de profesor
    /// Representa: "El profesor da [Materia] en [Curso]"
    /// </summary>
    public class TeacherAssignmentInfo
    {
        public Guid AssignmentId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int Year { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
    }

}

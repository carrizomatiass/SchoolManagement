using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Subjects.Models
{
    /// <summary>
    /// DTO para materia
    /// </summary>
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

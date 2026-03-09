using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // Matemática, Lengua, etc.
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
    }
}

using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Course : BaseEntity
    {
        public Guid AcademicYearId { get; set; }
        public Guid GradeId { get; set; }
        public Guid SectionId { get; set; }
        public string Name { get; set; } = string.Empty; // 1° A 2024
        public int Capacity { get; set; } // Cupo máximo de alumnos

        // Navigation properties
        public AcademicYear AcademicYear { get; set; } = null!;
        public Grade Grade { get; set; } = null!;
        public Section Section { get; set; } = null!;
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}

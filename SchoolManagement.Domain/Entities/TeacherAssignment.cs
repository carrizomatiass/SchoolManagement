using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class TeacherAssignment : BaseEntity
    {
        public Guid TeacherId { get; set; }
        public Guid CourseSubjectId { get; set; }
        public Guid AcademicYearId { get; set; }

        // Navigation properties
        public Teacher Teacher { get; set; } = null!;
        public CourseSubject CourseSubject { get; set; } = null!;
        public AcademicYear AcademicYear { get; set; } = null!;
    }
}

using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class GradeRecord : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseSubjectId { get; set; }
        public Guid TermId { get; set; }
        public decimal GradeValue { get; set; }
        public GradeStatus Status { get; set; }
        public string? Comments { get; set; }
        public Guid CreatedBy { get; set; } // Teacher Id

        // Navigation properties
        public Student Student { get; set; } = null!;
        public CourseSubject CourseSubject { get; set; } = null!;
        public Term Term { get; set; } = null!;
    }
}

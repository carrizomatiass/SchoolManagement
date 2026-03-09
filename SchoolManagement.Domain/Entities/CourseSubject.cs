using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class CourseSubject : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid SubjectId { get; set; }

        // Navigation properties
        public Course Course { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
    }
}

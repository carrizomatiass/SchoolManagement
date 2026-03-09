using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Comments { get; set; }
        public Guid CreatedBy { get; set; } // Teacher Id

        // Navigation properties
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}

using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Student : BaseEntity
    {
        public Guid UserId { get; set; }
        public string EnrollmentNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MedicalInfo { get; set; }
        public string? EmergencyContact { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}

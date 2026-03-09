using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public Guid UserId { get; set; }
        public string? Specialty { get; set; }
        public DateTime? HireDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}

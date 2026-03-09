using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class StudentParent : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
        public bool IsPrimaryContact { get; set; }

        // Navigation properties
        public Student Student { get; set; } = null!;
        public Parent Parent { get; set; } = null!;
    }
}

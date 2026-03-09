using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Parent : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Relationship { get; set; } = string.Empty; // padre, madre, tutor

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
    }
}

using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Section : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // A, B, C

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

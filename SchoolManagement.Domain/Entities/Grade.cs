using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Grade : BaseEntity
    {
        public string Level { get; set; } = string.Empty; // primary, secondary
        public int GradeNumber { get; set; } // 1, 2, 3, 4, 5, 6
        public string Name { get; set; } = string.Empty; // Primer Grado, Segundo Grado, etc.

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

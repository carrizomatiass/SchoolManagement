using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class AcademicYear : BaseEntity
    {
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Term> Terms { get; set; } = new List<Term>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

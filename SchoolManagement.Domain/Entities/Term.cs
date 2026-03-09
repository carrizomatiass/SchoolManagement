using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Term : BaseEntity
    {
        public Guid AcademicYearId { get; set; }
        public int TermNumber { get; set; } // 1, 2, 3
        public string Name { get; set; } = string.Empty; // Primer Trimestre, Segundo Trimestre, etc.
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation properties
        public AcademicYear AcademicYear { get; set; } = null!;
        public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
    }
}

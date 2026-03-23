using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Models
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EnrollmentNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public bool IsActive { get; set; }
    }
}

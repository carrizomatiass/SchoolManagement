using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Models
{
    public class ParentDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string? Phone { get; set; }
    }
}

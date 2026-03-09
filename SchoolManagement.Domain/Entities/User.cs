using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public Profile? Profile { get; set; }
        public Teacher? Teacher { get; set; }
        public Student? Student { get; set; }
        public Parent? Parent { get; set; }
    }
}

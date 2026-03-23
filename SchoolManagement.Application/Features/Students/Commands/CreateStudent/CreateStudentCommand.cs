using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent
{
    /// <summary>
    /// Comando para crear un nuevo alumno
    /// Crea el usuario, perfil y registro de alumno
    /// </summary>
    public class CreateStudentCommand : IRequest<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string EnrollmentNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MedicalInfo { get; set; }
        public string? EmergencyContact { get; set; }
    }

}

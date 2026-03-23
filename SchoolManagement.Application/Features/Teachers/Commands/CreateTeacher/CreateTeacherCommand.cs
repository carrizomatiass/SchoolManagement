using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Commands.CreateTeacher
{
    /// <summary>
    /// Comando para crear un nuevo profesor
    /// Crea tanto el usuario como el perfil y el registro de profesor
    /// </summary>
    public class CreateTeacherCommand : IRequest<Guid>
    {
        // Datos de usuario
        /// <summary>
        /// Email del profesor (será su usuario para login)
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña inicial del profesor
        /// </summary>
        public string Password { get; set; } = string.Empty;

        // Datos de perfil
        /// <summary>
        /// Nombre del profesor
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Apellido del profesor
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Número de documento (DNI, CI, etc.)
        /// </summary>
        public string? DocumentNumber { get; set; }

        /// <summary>
        /// Teléfono de contacto
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Dirección del profesor
        /// </summary>
        public string? Address { get; set; }

        // Datos específicos de profesor
        /// <summary>
        /// Especialidad del profesor (ejemplo: "Matemática", "Lengua")
        /// </summary>
        public string? Specialty { get; set; }

        /// <summary>
        /// Fecha de contratación
        /// </summary>
        public DateTime? HireDate { get; set; }
    }

}

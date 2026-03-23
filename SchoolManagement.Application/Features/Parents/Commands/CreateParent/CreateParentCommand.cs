using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Commands.CreateParent
{
    /// <summary>
    /// Comando para crear un padre/tutor
    /// Crea el usuario, perfil y registro de padre
    /// </summary>
    public class CreateParentCommand : IRequest<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Relationship { get; set; } = string.Empty;
    }
}

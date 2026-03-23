using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Commands.AssignParentToStudent
{
    /// <summary>
    /// Comando para asociar un padre/tutor con un alumno
    /// Permite marcar si es el contacto principal
    /// </summary>
    public class AssignParentToStudentCommand : IRequest<Guid>
    {
        public Guid ParentId { get; set; }
        public Guid StudentId { get; set; }
        public bool IsPrimaryContact { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Teachers.Commands.CreateTeacher
{
    /// <summary>
    /// Handler que procesa la creación de un nuevo profesor
    /// Crea: User, Profile y Teacher en una sola transacción
    /// </summary>
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public CreateTeacherCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            // Verificar si ya existe un usuario con ese email
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
                throw new InvalidOperationException($"Ya existe un usuario con el email {request.Email}");

            // Crear el usuario
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = UserRole.Teacher, // Rol de profesor
                IsActive = true
            };

            _context.Users.Add(user);

            // Crear el perfil
            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DocumentNumber = request.DocumentNumber,
                Phone = request.Phone,
                Address = request.Address
            };

            _context.Profiles.Add(profile);

            // Crear el registro de profesor
            var teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Specialty = request.Specialty,
                HireDate = request.HireDate ?? DateTime.UtcNow,
                IsActive = true
            };

            _context.Teachers.Add(teacher);

            await _context.SaveChangesAsync(cancellationToken);

            return teacher.Id;
        }
    }
}

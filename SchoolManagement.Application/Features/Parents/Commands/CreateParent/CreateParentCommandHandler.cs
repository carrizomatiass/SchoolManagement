using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Parents.Commands.CreateParent
{
    /// <summary>
    /// Handler que crea un nuevo padre/tutor
    /// Crea User, Profile y Parent en una transacción
    /// </summary>
    public class CreateParentCommandHandler : IRequestHandler<CreateParentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public CreateParentCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateParentCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
                throw new InvalidOperationException($"Ya existe un usuario con el email {request.Email}");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = UserRole.Parent,
                IsActive = true
            };

            _context.Users.Add(user);

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

            var parent = new Parent
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Relationship = request.Relationship
            };

            _context.Parents.Add(parent);

            await _context.SaveChangesAsync(cancellationToken);

            return parent.Id;
        }
    }
}

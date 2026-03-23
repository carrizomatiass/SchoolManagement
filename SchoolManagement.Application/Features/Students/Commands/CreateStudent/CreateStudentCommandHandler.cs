using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent
{
    /// <summary>
    /// Handler que crea un nuevo alumno
    /// Crea User, Profile y Student en una transacción
    /// </summary>
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public CreateStudentCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
                throw new InvalidOperationException($"Ya existe un usuario con el email {request.Email}");

            var existingEnrollment = await _context.Students
                .FirstOrDefaultAsync(s => s.EnrollmentNumber == request.EnrollmentNumber, cancellationToken);

            if (existingEnrollment != null)
                throw new InvalidOperationException($"Ya existe un alumno con el número de matrícula {request.EnrollmentNumber}");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = UserRole.Student,
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

            var student = new Student
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                EnrollmentNumber = request.EnrollmentNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                MedicalInfo = request.MedicalInfo,
                EmergencyContact = request.EmergencyContact,
                IsActive = true
            };

            _context.Students.Add(student);

            await _context.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}

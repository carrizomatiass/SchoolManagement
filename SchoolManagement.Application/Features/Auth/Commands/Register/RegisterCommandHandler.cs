using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Auth.Models;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IApplicationDbContext context,
        IJwtService jwtService,
        IPasswordHasher passwordHasher) 
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher; 
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Verificar si el email ya existe
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser != null)
            throw new InvalidOperationException("El email ya está registrado");

        // Crear usuario
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password), // 👈 CAMBIAR ESTA LÍNEA
            Role = request.Role,
            IsActive = true
        };

        _context.Users.Add(user);

        // Crear perfil
        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DocumentNumber = request.DocumentNumber,
            Phone = request.Phone
        };

        _context.Profiles.Add(profile);

        await _context.SaveChangesAsync(cancellationToken);

        // Generar token
        var token = _jwtService.GenerateToken(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
            Token = token,
            FullName = profile.FullName
        };
    }
}
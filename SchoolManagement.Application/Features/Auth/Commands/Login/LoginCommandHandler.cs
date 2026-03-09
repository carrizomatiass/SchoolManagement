using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Application.Features.Auth.Models;

namespace SchoolManagement.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IJwtService jwtService,
        IPasswordHasher passwordHasher) 
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher; 
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Credenciales inválidas");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Usuario inactivo");

        // Verificar contraseña usando el servicio
        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash)) // 👈 CAMBIAR ESTA LÍNEA
            throw new UnauthorizedAccessException("Credenciales inválidas");

        // Actualizar último login
        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        // Generar token
        var token = _jwtService.GenerateToken(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
            Token = token,
            FullName = user.Profile?.FullName ?? user.Email
        };
    }
}
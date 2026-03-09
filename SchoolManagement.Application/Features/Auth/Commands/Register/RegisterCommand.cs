using MediatR;
using SchoolManagement.Application.Features.Auth.Models;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Phone { get; set; }
}
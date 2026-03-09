using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Auth.Models;

public class AuthResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;

namespace HostMarket.Core.Services.Interfaces;

public record AuthResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public string? Message { get; init; }
    public Guid UserId { get; init; }
    public string? Token { get; init; }
    public int Code { get; init; }
    public UserRole? Role { get; init; }

}

/// <summary>
/// Service for authentication operations
/// </summary>
public interface IAuthenticationService
{
    Task<AuthResult> SignUpAsync(UserRegisterDto dto);
    Task<AuthResult> SignInAsync(UserLoginDTO dto);
    Task<AuthResult> VerificationAsync(VerificationDto dto);
    Task<AuthResult> DeleteAsync(Guid userid);
}

using HostMarket.Core.Repositories;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HostMarket.Infrastructure.Auth;

public class AuthService : IAuthenticationService
{
    // Creating and defining private fields in the constructor
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    // ----- Main Part -----

    // SingIn Function
    public async Task<AuthResult> SignInAsync(UserLoginDTO loginDTO)
    {
        try
        {
            var users = await _userRepository.GetAllAsync();   // Get All users from DB
            var user = users.FirstOrDefault(u => u.Email == loginDTO.Email);

            // null result checking
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            // Genering jwt-token 
            var token = GenerateTokenAsync(user.Id);

            // return the report
            return new AuthResult
            {
                Success = true,
                UserId = user.Id,
                Token = token.ToString()
            };
        }

        catch (Exception ex)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "An error occurred during sign in"
            };
        }
    }


    // SingUp function
    public async Task<AuthResult> SignUpAsync(UserRegisterDto registerDto)
    {
        try
        {
            // Get all users 
            var users = await _userRepository.GetAllAsync();

            // if user exist checking
            if (users.Any(u => u.Email == registerDto.Email))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User with this email already exists"
                };
            }

            // Creating new userDto
            var userId = Guid.NewGuid();
            var user = new UserDTO
            {
                Id = userId,
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            // Add a user to the DB
            await _userRepository.CreateAsync(user);

            // Genering jwt-token
            var token = await GenerateTokenAsync(userId);

            // Return the report
            return new AuthResult
            {
                Success = true,
                UserId = userId,
                Token = token
            };
        }

        catch (Exception ex)
        {
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "An error occurred during sign up"
            };
        }
    }


    // SignOut function
    public Task<bool> SignOutAsync(string token)
    {
        return Task.FromResult(true);
    }


    // ----- Sup part -----

    // Generate jwt-token function
    private async Task<string> GenerateTokenAsync(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:JwtSecret"] ?? string.Empty);

        // creating a claims list for jwt
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, userId.ToString())
        };

        // Claim - Username
        var user = await _userRepository.GetByIdAsync(userId);
        claims.Add(new Claim("UserName", user.UserName));

        // Creating jwt
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
        };

        // return token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    // // Token validation
    // public Task<bool> ValidateTokenAsync(string token)
    // {
    //     try
    //     {
    //         var tokenHandler = new JwtSecurityTokenHandler();
    //         var key = Encoding.ASCII.GetBytes(_configuration.["AppSettings:JwtSecret"] ?? string.Empty);

    //         tokenHandler.ValidateToken(token, new TokenValidationParameters
    //         {
    //             ValidateIssuerSigningKey = true,
    //             IssuerSigningKey = new SymmetricSecurityKey(key),
    //             ValidateIssuer = false,
    //             ValidateAudience = false,
    //             ValidateLifetime = true,
    //             ClockSkew = TimeSpan.Zero
    //         }, out _);

    //         return Task.FromResult(true);
    //     }

    //     catch
    //     {
    //         return Task.FromResult(false);
    //     }
    // }
    
}

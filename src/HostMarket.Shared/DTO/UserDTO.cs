using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Shared.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public string Code { get; set; }
        public decimal Balance { get; set; }
        public bool IsVerify { get; set; }
        public DateTime RegistrationDate { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public Status Status { get; set; }
    }

    public record VerificationDto
    {
        public string Email { get; init; }
        public string Code { get; init; }
    }
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    } 
    public class UserRegisterDto
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
    }
}

using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public bool IsVerify { get; set; }
        public DateTime RegistrationDate { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public Status Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.DTO;

public class UserRegisterDto
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

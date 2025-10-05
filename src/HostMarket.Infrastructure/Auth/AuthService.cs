using System;
using System.Collections.Generic;
using System.Linq;
using HostMarket.Core.Repositories;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.DTO;
using HostMarket.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Auth
{
    public class AuthService : IAuthenticationService
    {

    }
}
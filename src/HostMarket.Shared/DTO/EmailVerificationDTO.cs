using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Shared.DTO
{
    public class EmailVerificationDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

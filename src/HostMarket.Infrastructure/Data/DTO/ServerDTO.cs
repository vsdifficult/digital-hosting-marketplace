using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.DTO
{
    public class ServerDTO
    {
        public Guid ownerId { get; set; }
        public string ServerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServerStatus ServStatus { get; set; }
    }
    
}

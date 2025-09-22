using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HostMarket.Infrastructure.Data.Entities
{
    public class ServerEntity : BaseEntity
    {
        public string ServerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServerStatus Status { get; set; }
    }
}

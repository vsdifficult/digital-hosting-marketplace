using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.Entities
{
    public class TariffEntity : BaseEntity
    {
        public string TariffName { get; set; }
        public decimal RamGb { get; set; }
        public decimal DiskGb { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<ServerEntity>? Servers { get; set; } = new List<ServerEntity>();
    }
}

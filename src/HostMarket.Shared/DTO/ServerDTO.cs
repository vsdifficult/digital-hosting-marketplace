using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.DTO
{
    public class ServerDTO
    {
        public Guid Id { get; set; }
        public Guid? ownerId { get; set; }
        public Guid TariffId { get; set; }
        public string ServerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServerStatus ServStatus { get; set; } 
        public int Port {get; set;} 
        public string IP {get; set;} 
        public string ContainerId {get; set;}

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public Status Status { get; set; }
    }
    
}


using HostMarket.Shared.Models;

namespace HostMarket.Infrastructure.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }  
        
        public Status Status { get; set; }
    }
}

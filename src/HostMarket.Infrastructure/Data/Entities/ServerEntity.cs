using HostMarket.Shared.Models; 

namespace HostMarket.Infrastructure.Data.Entities
{
    public class ServerEntity : BaseEntity
    {
        public Guid? ownerId { get; set; }
        public Guid TariffId { get; set; }
        public string ServerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServerStatus ServStatus { get; set; }

        
        // Navigation 
        
        public UserEntity? User { get; set; }
        public TariffEntity Tariff { get; set; }
    }
}

using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Shared.DTO
{
    public class TariffDto
    {
        public Guid Id { get; set; }
        public string TariffName { get; set; }
        public decimal RamGb { get; set; }
        public decimal DiskGb { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public Status Status { get; set; }
    }
    public class CreateTariffDto
    {
        public string TariffName { get; set; }
        public decimal RamGb { get; set; }
        public decimal DiskGb { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBerth.Domain.Entities
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Size { get; set; } // Length in meters
        public string Type { get; set; } = string.Empty;

        public ICollection<DockingRecord> DockingRecords { get; set; } = new List<DockingRecord>();

    }
}

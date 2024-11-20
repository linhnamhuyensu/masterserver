using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.Domain.Models.Game
{
    public class PlayerInventoryDto
    {
        public string InventoryItemId { get; set; }
        public int Quantity { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Dictionary<string, object> CustomData { get; set; }

        public Instant CreatedAt { get; set; }

        public Instant? UpdatedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterServer.Domain.Entities.Economy
{
    public class PlayerInventory : BaseAuditableEntity
    {
        [ForeignKey(nameof(InventoryItemId))]
        public InventoryItemDefinition InventoryItemDefinition { get; set; }

        public string InventoryItemId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        public int PlayerId { get; set; }
        public int Quantity { get; set; }
        public Dictionary<string, object> CustomData { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}

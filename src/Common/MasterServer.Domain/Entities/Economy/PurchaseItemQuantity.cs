using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.Domain.Entities.Economy
{
    public class PurchaseItemQuantity : BaseAuditableEntity
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey(nameof(EconomyDefintionId))]
        public BaseEconomyDefinition EconomyDefintion { get; set; }

        public string EconomyDefintionId { get; set; }

        public int Amount { get; set; }
    }
}

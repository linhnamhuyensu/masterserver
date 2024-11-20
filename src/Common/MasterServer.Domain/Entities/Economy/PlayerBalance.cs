using MasterServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.Domain.Entities.Economy
{
    public class PlayerBalance : BaseAuditableEntity
    {

        [ForeignKey(nameof(CurrencyId))]
        public CurrencyDefinition CurrencyDefinition { get; set; }

        public string CurrencyId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        public int PlayerId     { get; set; }
        public int Balance { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}

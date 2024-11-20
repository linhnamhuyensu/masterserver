using MasterServer.Domain.Entities.Economy;
using MasterServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace MasterServer.Domain.Models.Game
{
    public class PlayerBalanceDto
    {
        public string CurrencyId { get; set; }
        public int Balance { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Instant CreatedAt { get; set; }

        public Instant? UpdatedAt { get; set; }
    }
}

using MasterServer.Application.Common.Interfaces;
using MasterServer.Application.Common.Models;
using MasterServer.Domain.Entities.Economy;
using MasterServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MasterServer.Application.Game.Economy.Queries
{
    public class SyncConfigurationQuery : IRequestWrapper<SyncConfigurationResponse>
    {
    }

    public class SyncConfigurationResponse
    {
        public List<CurrencyDefinitionDto> CurrencyDefinitions { get; set; } = new List<CurrencyDefinitionDto> { };

        public List<InventoryItemDefinitionDto> InventoryItemDefinitions { get; set; } = new List<InventoryItemDefinitionDto> { };

        public List<RealMoneyPurchaseDefinitionDto> RealMoneyPurchaseDefinitions { get; set; } = new List<RealMoneyPurchaseDefinitionDto> { };
        public List<VirtualPurchaseDefinitionDto> VirtualPurchaseDefinitions { get; set; } = new List<VirtualPurchaseDefinitionDto>();
    }

    public class BaseEconomyDefinitionDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Initial { get; set; }

        public int Max { get; set; }

        public Dictionary<string, object> CustomData { get; set; }
        public virtual EconomyType EconomyType { get; set;  }
    }
    public class CurrencyDefinitionDto : BaseEconomyDefinitionDto
    {

    }

    public class InventoryItemDefinitionDto : BaseEconomyDefinitionDto
    {

    }

    public class RealMoneyPurchaseDefinitionDto : BaseEconomyDefinitionDto
    {
        public List<StoreIdentiferDto> StoreIdentifers { get; set; }
        public List<VirtualPurchaseCostDto> Rewards { get; set; }
    }

    public class VirtualPurchaseDefinitionDto : BaseEconomyDefinitionDto
    {
        public List<VirtualPurchaseCostDto> Rewards { get; set; }

        public List<VirtualPurchaseCostDto> Costs { get; set; }
    }

    public class StoreIdentiferDto
    {
        public string ProductId { get; set; }

        public PublisherPlatform Platform { get; set; }
    }
    public class VirtualPurchaseCostDto
    {
        public string EconomyDefintionId { get; set; }

        public int Amount { get; set; }
    }
    public class SyncConfigurationQueryHandler : IRequestHandlerWrapper<SyncConfigurationQuery, SyncConfigurationResponse>
    {

        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<SyncConfigurationQueryHandler> _logger;

        public SyncConfigurationQueryHandler(IApplicationDbContext dbContext, ILogger<SyncConfigurationQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<ServiceResult<SyncConfigurationResponse>> Handle(SyncConfigurationQuery request, CancellationToken cancellationToken)
        {
            var syncConfigurationResponse = new SyncConfigurationResponse();
            syncConfigurationResponse.CurrencyDefinitions = await _dbContext.CurrencyDefinitions.Select(x => new CurrencyDefinitionDto
            {
                CustomData = x.CustomData,
                EconomyType = x.EconomyType,
                Id = x.Id,
                Initial = x.Initial,
                Max = x.Max,
                Name = x.Name,
                

            }).ToListAsync();

            syncConfigurationResponse.InventoryItemDefinitions = await _dbContext.InventoryItemDefinitions.Select(x => new InventoryItemDefinitionDto
            {
                CustomData = x.CustomData,
                EconomyType = x.EconomyType,
                Id = x.Id,
                Initial = x.Initial,
                Max = x.Max,
                Name = x.Name,
            }).ToListAsync();

            syncConfigurationResponse.RealMoneyPurchaseDefinitions = await _dbContext.RealMoneyPurchaseDefinitions.Select(x => new RealMoneyPurchaseDefinitionDto
            {
                CustomData = x.CustomData,
                EconomyType = x.EconomyType,
                Id = x.Id,
                Initial = x.Initial,
                Max = x.Max,
                Name = x.Name,
                Rewards = x.Rewards.Select(y => new VirtualPurchaseCostDto
                {
                    Amount = y.PurchaseItemQuantity.Amount,
                    EconomyDefintionId = y.PurchaseItemQuantity.EconomyDefintionId
                }).ToList(),
                StoreIdentifers = x.StoreIdentifers.Select(y => new StoreIdentiferDto
                {
                    Platform = y.Platform,
                    ProductId = y.ProductId,
                }).ToList()
            }).ToListAsync();

            syncConfigurationResponse.VirtualPurchaseDefinitions = await _dbContext.VirtualPurchaseDefinitions.Select(x => new VirtualPurchaseDefinitionDto
            {
                CustomData = x.CustomData,
                EconomyType = x.EconomyType,
                Id = x.Id,
                Initial = x.Initial,
                Max = x.Max,
                Name = x.Name,
                Rewards = x.Rewards.Select(y => new VirtualPurchaseCostDto
                {
                    Amount = y.Amount,
                    EconomyDefintionId = y.EconomyDefintionId
                }).ToList(),
                Costs = x.Costs.Select(y => new VirtualPurchaseCostDto
                {
                    Amount = y.Amount,
                    EconomyDefintionId = y.EconomyDefintionId
                }).ToList()
            }).ToListAsync();
            return ServiceResult.Success(syncConfigurationResponse);
        }

    }
}

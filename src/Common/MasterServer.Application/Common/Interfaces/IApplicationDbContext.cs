using MasterServer.Domain.Entities;
using MasterServer.Domain.Entities.BattlePasses;
using MasterServer.Domain.Entities.Economy;
using MasterServer.Domain.Entities.Game;
using MasterServer.Domain.Entities.GameNotification;
using MasterServer.Domain.Entities.Mails;
using MasterServer.Domain.Entities.Socials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
namespace MasterServer.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; set; }

    DbSet<RefreshToken> RefreshTokens { get; set; }
    DbSet<GameSession> GameSessions { get; set; }
    DbSet<GameMatch> GameMatches { get; set; }

    DbSet<GameNotification> GameNotifications { get; set; }
    DbSet<GameMail> GameMails { get; set; }
    DbSet<MailAttachment> MailAttachments { get; set; }
    DbSet<PlayerMail> PlayerMails { get; set; }

    DbSet<Conversation> Conversations { get; set; }
    DbSet<ConversationMessage> ConversationMessages { get; set; }
    DbSet<ConversationParticipant> ConversationParticipants { get; set; }

    DbSet<PlayerEdge> PlayerEdges { get; set; }

    DbSet<GameKvSettingConfig> GameKvSettingConfigs { get; set; }

    DbSet<BattlePassDefinition> BattlePassSeasons { get; set; }

    DbSet<BattlePassSeasonParticipant> BattlePassParticipants { get; set; }

    DbSet<BattlePassSeasonParticipantMilestone> BattlePassParticipantMilestones { get; set; }

    DbSet<BattlePassMilestone> BattlePassMilestones { get; set; }

    DbSet<BaseEconomyDefinition> BaseEconomyDefinitions { get; set; }

    DbSet<CurrencyDefinition> CurrencyDefinitions { get; set; }

    DbSet<InventoryItemDefinition> InventoryItemDefinitions { get; set; }

    DbSet<PlayerBalance> PlayerBalances { get; set; }

    DbSet<PlayerInventory> PlayerInventories { get; set; }

    DbSet<PlayerShopRealMoneyPurchaseReceipt> PlayerShopRealMoneyPurchaseReceipts { get; set; }

    DbSet<PlayerShopVirtualPurchaseReceipt> PlayerShopVirtualPurchaseReceipts { get; set; }

    DbSet<PurchaseItemQuantity> PurchaseItemQuantities { get; set; }

    DbSet<RealMoneyPurchaseDefinition> RealMoneyPurchaseDefinitions { get; set; }

    DbSet<RealMoneyPurchaseReward> RealMoneyPurchaseRewards { get; set; }

    DbSet<StoreIdentifer> StoreIdentifers { get; set; }

    DbSet<VirtualPurchaseCost> VirtualPurchaseCosts { get; set; }

    DbSet<VirtualPurchaseDefinition> VirtualPurchaseDefinitions { get; set; }

    DbSet<VirtualPurchaseReward> VirtualPurchaseRewards { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    DatabaseFacade Database
    {
        get;
    }


    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<TEntity> Set<TEntity>(string name) where TEntity : class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry Entry(object entity);


    void SetModified(object entity);

    void SetPropertyModified<T>(T entity, string selectProperty);

    IDbContextTransaction StartTransaction(IsolationLevel isolationLevel);

    ChangeTracker ChangeTracker { get; }

    void ClearChangeTracker();

}

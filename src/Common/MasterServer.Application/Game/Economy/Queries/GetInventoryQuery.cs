using MasterServer.Application.Common.Extensions;
using MasterServer.Application.Common.Interfaces;
using MasterServer.Application.Common.Models;
using MasterServer.Domain.Models.Game;
using Microsoft.Extensions.Logging;

namespace MasterServer.Application.Game.Economy.Queries
{
    public class GetInventoryQuery : BaseAuthRequest, IRequestWrapper<PaginatedList<PlayerInventoryDto>>
    {
        public BodyPaginationParameter PaginationParameter { get; set; }
    }

    public class GetInventoryQueryHandler : IRequestHandlerWrapper<GetInventoryQuery, PaginatedList<PlayerInventoryDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetInventoryQueryHandler> _logger;

        public GetInventoryQueryHandler(IApplicationDbContext dbContext, ILogger<GetInventoryQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<ServiceResult<PaginatedList<PlayerInventoryDto>>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            var paginatedList = await _dbContext.PlayerInventories.Where(x => x.PlayerId == request.PlayerId.Value).OrderBy(x => x.InventoryItemId).Select(x => new PlayerInventoryDto
            {
                Quantity = x.Quantity,
                InventoryItemId = x.InventoryItemId,
                CustomData = x.CustomData,
                CreatedAt = x.CreatedAt,
                Timestamp = x.Timestamp,
                UpdatedAt = x.UpdatedAt
            }).ToPaginationAsync(request.PaginationParameter, true);
            return ServiceResult.Success(paginatedList);
        }
    }
}

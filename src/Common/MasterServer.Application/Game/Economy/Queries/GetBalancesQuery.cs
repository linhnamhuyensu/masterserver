using MasterServer.Application.Common.Extensions;
using MasterServer.Application.Common.Interfaces;
using MasterServer.Application.Common.Models;
using MasterServer.Domain.Models.Game;
using Microsoft.Extensions.Logging;

namespace MasterServer.Application.Game.Economy.Queries
{
    public class GetBalancesQuery : BaseAuthRequest, IRequestWrapper<PaginatedList<PlayerBalanceDto>>
    {
        public BodyPaginationParameter PaginationParameter { get; set; }
    }

    public class GetBalanceQueryHandler : IRequestHandlerWrapper<GetBalancesQuery, PaginatedList<PlayerBalanceDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetBalanceQueryHandler> _logger;

        public GetBalanceQueryHandler(IApplicationDbContext dbContext, ILogger<GetBalanceQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<ServiceResult<PaginatedList<PlayerBalanceDto>>> Handle(GetBalancesQuery request, CancellationToken cancellationToken)
        {
            var paginatedList = await _dbContext.PlayerBalances.Where(x => x.PlayerId == request.PlayerId.Value).OrderBy(x => x.CurrencyId).Select(x => new PlayerBalanceDto
            {
                Balance = x.Balance,
                CurrencyId = x.CurrencyId,
                CreatedAt = x.CreatedAt,
                Timestamp = x.Timestamp,
                UpdatedAt = x.UpdatedAt
            }).ToPaginationAsync(request.PaginationParameter, true);
            return ServiceResult.Success(paginatedList);
        }
    }
}

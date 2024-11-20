using MasterServer.Application.Common.Interfaces;
using MasterServer.Application.Common.Models;
using MasterServer.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MasterServer.Application.Game.Socket.Commands
{
    public class SocketUserDisconnectedCommand : BaseAuthRequest, IRequestWrapper<EmptyServiceResponse>
    {

        public int ConnectionId { get; set; }

        public Guid SessionId { get; set; }
    }

    public class SocketUserDisconnectedCommandHandler : IRequestHandlerWrapper<SocketUserDisconnectedCommand, EmptyServiceResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<SocketUserDisconnectedCommandHandler> _logger;

        public SocketUserDisconnectedCommandHandler(IApplicationDbContext dbContext, ILogger<SocketUserDisconnectedCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ServiceResult<EmptyServiceResponse>> Handle(SocketUserDisconnectedCommand request, CancellationToken cancellationToken)
        {
            var player = await _dbContext.Players.Where(x => x.Id == request.PlayerId.Value).FirstOrDefaultAsync();
            var gameSession = await _dbContext.GameSessions.Where(x => x.Id == request.SessionId).FirstOrDefaultAsync();
            gameSession.DisconnectedAt = DateTimeHelper.InstanceNow;
            player.LatestOfflineAt = DateTimeHelper.InstanceNow;
            await _dbContext.SaveChangesAsync();
            return EmptyServiceResponse.Create();
        }
    }
}

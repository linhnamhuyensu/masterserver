using MasterServer.Application.Common.Exceptions;
using MasterServer.Application.Common.Interfaces;
using MasterServer.Application.Common.Models;
using MasterServer.Application.Helpers;
using MasterServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace MasterServer.Application.Game.Authentication.Commands
{
    public class RefreshAccessTokenCommand : IRequestWrapper<RefreshTokenResponse>
    {
        public string Version { get; set; }

        public string RefreshToken { get; set; }

        public string RemoteIpAddress { get; set; }

        public string CountryCode { get; set; }
    }

    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandlerWrapper<RefreshAccessTokenCommand, RefreshTokenResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDateTimeService _dateTime;
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _jwtUtils;
        private readonly IWebsocketNofiticationService _websocketNofiticationService;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;
        public RefreshTokenCommandHandler(IApplicationDbContext dbContext, IConfiguration configuration, IDateTimeService dateTime, IIdentityService identityService, IFeatureManagerSnapshot featureHubConfig, ILogger<RefreshTokenCommandHandler> logger, IJwtUtils jwtUtils, ICurrentPlayerService currentPlayerService, IWebsocketNofiticationService websocketNofiticationService)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
            _identityService = identityService;
            _configuration = configuration;
            _logger = logger;
            _jwtUtils = jwtUtils;
            _websocketNofiticationService = websocketNofiticationService;
        }
        public async Task<ServiceResult<RefreshTokenResponse>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var refreshToken = await _dbContext.RefreshTokens.Include(x => x.Player).Where(x => x.Token == request.RefreshToken).FirstOrDefaultAsync();
                if (refreshToken == null)
                {
                    throw new NotFoundException(nameof(RefreshToken), $"refreshtoken_{request.RefreshToken}");
                }

                if (!refreshToken.IsActive)
                {
                    throw new ValidationException("refreshtoken", "refreshtoken_is_not_active");
                }

                var player = refreshToken.Player;
                refreshToken = _jwtUtils.GenerateRefreshToken(request.RemoteIpAddress.ToString());
                player.RefreshTokens.Add(refreshToken);
                await _dbContext.RefreshTokens.Where(x => x.Player == player).OrderByDescending(x => x.Expires).Skip(5).ExecuteDeleteAsync();

                await _dbContext.SaveChangesAsync();
                var jwtToken = _jwtUtils.GenerateJwtToken(request.Version, player);

                var playerLoginResponse = new RefreshTokenResponse()
                {
                    AccessToken = jwtToken,
                    RefreshToken = refreshToken.Token,
                };
                return ServiceResult.Success(playerLoginResponse);
            }
            catch (Exception ex)
            {
                return ServiceResult.Failed<RefreshTokenResponse>(ServiceError.AuthenticationCredentialIsNotCorrect);
            }
        }
    }

}

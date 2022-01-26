using Orleans.Api.Application.Interfaces;
using Orleans.Api.Domain;
using Orleans.Api.Grains.Interfaces;

namespace Orleans.Api.Application
{
    public class PlayerService : IPlayerService
    {
        private readonly IGrainFactory _grainFactory;

        public PlayerService(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        public Task CreatePlayerAsync(Player player)
        {
            var playerGrain = _grainFactory.GetGrain<IPlayerGrain>(player.Id);

            return playerGrain.CreatePlayerAsync(player);
        }

        public Task<Player> GetPlayerAsync(int playerId)
        {
            var playerGrain = _grainFactory.GetGrain<IPlayerGrain>(playerId);

            return playerGrain.GetPlayerAsync();
        }

        public Task<bool> UpdateLevelAsync(int playerId, LevelAction levelAction)
        {
            var playerGrain = _grainFactory.GetGrain<IPlayerGrain>(playerId);

            return playerGrain.UpdateLevelAsync(levelAction);
        }
    }
}

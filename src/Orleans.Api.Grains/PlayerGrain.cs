using Orleans.Api.Domain;
using Orleans.Api.Grains.Interfaces;

namespace Orleans.Api.Grains
{
    public class PlayerGrain : Grain, IPlayerGrain
    {
        private Player _player;

        public Task CreatePlayerAsync(Player player)
        {
            _player = player;

            return Task.CompletedTask;
        }

        public Task<Player> GetPlayerAsync()
        {
            return Task.FromResult(_player);
        }

        public Task<bool> UpdateLevelAsync(LevelAction levelAction)
        {
            if (_player == null)
                return Task.FromResult(false);

            if (levelAction == LevelAction.LevelUp)
            {
                _player.LevelUp();
            }
            else
            {
                _player.LevelDown();
            }

            return Task.FromResult(true);
        }

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }
    }
}

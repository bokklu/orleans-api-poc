using Orleans.Api.Domain;

namespace Orleans.Api.Grains.Interfaces
{
    public interface IPlayerGrain : IGrainWithIntegerKey
    {
        Task CreatePlayerAsync(Player player);
        Task<Player> GetPlayerAsync();
        Task<bool> UpdateLevelAsync(LevelAction levelAction);
    }
}

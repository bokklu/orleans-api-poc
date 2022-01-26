using Orleans.Api.Domain;

namespace Orleans.Api.Application.Interfaces
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(Player player);
        Task<Player> GetPlayerAsync(int playerId);
        Task<bool> UpdateLevelAsync(int playerId, LevelAction levelAction);
    }
}
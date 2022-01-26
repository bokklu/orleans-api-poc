namespace Orleans.Api.Infra.Controllers.Extensions
{
    public static class InputToDomain
    {
        public static Domain.Player ToDomain(this Models.Input.Player player)
        {
            return Domain.Player.Create(player.Id, player.Name, (Domain.PlayerLevel) player.Level);
        }
    }
}

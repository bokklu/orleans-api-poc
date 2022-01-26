using Orleans.Api.Models.Common;

namespace Orleans.Api.Models.Input
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerLevel Level { get; set; }
    }
}

namespace Orleans.Api.Domain
{
    public class Player
    {
        public int Id { get; }
        public string Name { get; }
        public PlayerLevel Level { get; private set; }

        private Player(int id, string name, PlayerLevel level)
        {
            Id = id;
            Name = name;
            Level = level;
        }

        public static Player Create(int id, string name, PlayerLevel level)
        {
            return new Player(id, name, level);
        }

        #region Mutators

        public void LevelUp()
        {
            if (Level != PlayerLevel.Master)
                ++Level;
        }

        public void LevelDown()
        {
            if (Level != PlayerLevel.Beginner)
                --Level;
        }

        #endregion

    }
}
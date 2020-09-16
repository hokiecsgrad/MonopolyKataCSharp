namespace MonopolyKata
{
    public class Player
    {
        public string Name { get; set; }
        public int Rounds;

        public Player(string name)
        {
            Name = name;
            Rounds = 0;
        }
    }
}
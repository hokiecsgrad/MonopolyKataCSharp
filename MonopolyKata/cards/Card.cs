namespace MonopolyKata.Cards
{
    public abstract class Card
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract void Execute(Player player);
    }
}
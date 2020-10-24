namespace MonopolyKata.Cards
{
    public abstract class Card
    {
        public Deck DeckReference { get; set; }
        public Board BoardReference { get; set; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract void Execute(Player player);
    }
}
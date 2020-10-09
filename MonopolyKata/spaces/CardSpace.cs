using MonopolyKata.Cards;

namespace MonopolyKata.Spaces
{
    public class CardSpace : Space
    {
        public override string Name { get; }
        public Deck Deck { get; }

        public CardSpace(string name, Deck deck)
        {
            Name = name;
            Deck = deck;
        }

        public override void LandedOnBy(Player player)
        {
            Card card = Deck.Draw();
            card.Execute(player);
        }
    }
}
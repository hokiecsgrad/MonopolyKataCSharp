using Microsoft.Extensions.Logging;
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
            BoardReference._logger?.LogInformation("{0} landed on {1} and draws a card.", player.Name, Name);

            Card card = Deck.Draw();

            BoardReference._logger?.LogInformation("{0} drew {1}.", player.Name, card.Name);
            BoardReference._logger?.LogInformation(card.Description);

            card.Execute(player);
        }
    }
}
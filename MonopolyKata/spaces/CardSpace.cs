using System;
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
            Card card = Deck.Draw();

            BoardReference?._logger?.LogInformation("{0} drew the '{1}' card.", player.Name, card.Name);
            BoardReference?._logger?.LogInformation(card.Description);

            try
            {
                card.Execute(player);
            }
            catch (NotImplementedException)
            {
                BoardReference?._logger?.LogWarning("This card has not yet been implemented.");
            }

            Deck.Add(card);
        }
    }
}
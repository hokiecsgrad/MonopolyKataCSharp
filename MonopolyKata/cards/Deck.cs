using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.Cards
{
    public class Deck
    {
        private Random rng;
        public List<Card> Cards { get; set; } = new List<Card>();

        public Deck(List<Card> cards)
        {
            rng = new Random();
            Cards = cards;
            foreach (Card card in Cards)
            {
                card.DeckReference = this;
            }
        }

        public void Shuffle()
        {
            Cards = Cards.OrderBy(a => rng.Next()).ToList();
        }

        public Card Draw()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        public void Add(Card card)
        {
            Cards.Add(card);
        }
    }
}
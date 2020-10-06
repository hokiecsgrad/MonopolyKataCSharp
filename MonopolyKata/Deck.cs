using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Deck
    {
        private Random rng;
        public List<Card> Cards { get; set; }

        public Deck(List<Card> cards)
        {
            rng = new Random();
            Cards = cards;
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata.Cards
{
    public class Deck
    {
        private Random rng;
        public List<Card> Cards { get; set; } = new List<Card>();

        private Board _boardRef = null;
        public Board BoardReference 
            { 
                get { return _boardRef; }

                set {
                    _boardRef = value;
                    foreach (Card card in Cards)
                        card.BoardReference = _boardRef;
                }
            }

        public Deck(List<Card> cards, Board boardReference = null)
        {
            rng = new Random();
            BoardReference = boardReference;
            Cards = cards;
            foreach (Card card in Cards)
            {
                card.DeckReference = this;
                card.BoardReference = BoardReference;
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
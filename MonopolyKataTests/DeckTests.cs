using MonopolyKata;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MonopolyKataTests
{
    public class DeckTests
    {
        List<Card> fake;
        Deck deck;

        public DeckTests()
        {
            fake = new List<Card> {
                new Card("1"), new Card("2"), new Card("3"), new Card("4"), 
                new Card("5"), new Card("6"), new Card("7"), new Card("8"),
                new Card("9"), new Card("10"), new Card("11"), new Card("12"),
                new Card("13"), new Card("14"), new Card("15"), new Card("16") 
                };
            deck = new Deck(fake);
        }

        [Fact]
        public void Shuffle_SixteenCards_ShouldRandomizeDeckOrder()
        {
            deck.Shuffle();

            Assert.Equal(16, deck.Cards.Count);
            Assert.False(fake.SequenceEqual(deck.Cards));
        }

        [Fact]
        public void Draw_Card_ShouldTakeFromTopOfDeck()
        {
            Card card = deck.Draw();

            Assert.Equal("1", card.Name);
            Assert.Equal(15, deck.Cards.Count);
        }

        [Fact]
        public void Draw_Card_ShouldPutCardAtBottomOfDeck()
        {
            deck.Add( deck.Draw() );

            Assert.Equal(16, deck.Cards.Count);
            Assert.Equal("2", deck.Cards[0].Name);
            Assert.Equal("1", deck.Cards[deck.Cards.Count-1].Name);
        }
    }
}

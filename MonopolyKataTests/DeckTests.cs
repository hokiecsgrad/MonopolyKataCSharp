using MonopolyKata;
using MonopolyKata.Cards;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MonopolyKataTests
{
    public class FakeCard : Card
    {
        public override string Name { get; }
        public override string Description { get; }

        public FakeCard(string name)
        {
            Name = name;
        }

        public override void Execute(Player player) { }
    }

    public class DeckTests
    {
        List<Card> fake;
        Deck deck;

        public DeckTests()
        {
            fake = new List<Card> {
                new FakeCard("1"), new FakeCard("2"), new FakeCard("3"), new FakeCard("4"), 
                new FakeCard("5"), new FakeCard("6"), new FakeCard("7"), new FakeCard("8"),
                new FakeCard("9"), new FakeCard("10"), new FakeCard("11"), new FakeCard("12"),
                new FakeCard("13"), new FakeCard("14"), new FakeCard("15"), new FakeCard("16") 
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

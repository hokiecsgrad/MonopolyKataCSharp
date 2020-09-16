using MonopolyKata;
using System;
using System.Collections.Generic;
using Xunit;

namespace MonopolyKataTests
{
    public class MonopolyTests
    {
        Player horse = new Player("Horse");
        Player car = new Player("Car");
        Monopoly game;

        public MonopolyTests()
        {
            horse = new Player("Horse");
            car = new Player("Car");
        }

        [Fact]
        public void Construct_Game_ShouldCreateGame()
        {
            game = new Monopoly(
                new List<Player>() { }
                );
            Assert.IsType<Monopoly>(game);
        }
        [Fact]
        public void Setup_GameWith2Players_ShouldWork()
        {
            game = new Monopoly(
                new List<Player> { horse, car }
                );

            Assert.Contains(horse, game.Players);
            Assert.Contains(car, game.Players);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        [InlineData(15)]
        public void Setup_GameWithWrongNumberOfPlayers_ShouldThrowException(int numPlayers)
        {
            List<Player> players = new List<Player>() { };

            for (int i = 0; i < numPlayers; i++)
                players.Add(new Player(i.ToString()));

            game = new Monopoly(players);

            Assert.Throws<InvalidOperationException>(() => game.Start());
        }

        [Fact]
        public void Setup_GameWith2Players_ShouldRandomizeOrder()
        {
            bool horseFirst = false;
            bool carFirst = false;

            for (int i = 0; i < 100; i++)
            {
                game = new Monopoly(
                    new List<Player> { horse, car }
                    );
                if (game.Players[0].Name == "Horse") horseFirst = true;
                if (game.Players[0].Name == "Car") carFirst = true;
            }

            Assert.True(horseFirst && carFirst);
        }

        [Fact]
        public void Rounds_GameWith2Players_ShouldPlayFor20Rounds()
        {
            game = new Monopoly(
                new List<Player> { horse, car }
                );
            game.Start();

            Assert.Equal(20, game.Rounds);
            Assert.Equal(20, horse.Rounds);
            Assert.Equal(20, car.Rounds);
        }

        [Fact(Skip = "I don't know how to test this")]
        public void Rounds_GameWith2Players_ShouldMaintainInitialOrderForAllRounds()
        {
            game = new Monopoly(
                new List<Player> { horse, car }
                );
            game.Start();
        }
    }
}
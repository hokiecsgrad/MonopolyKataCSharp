using MonopolyKata;
using System;
using System.Collections.Generic;
using Xunit;

namespace MonopolyKataTests
{
    public class MonopolyTests
    {
        [Fact]
        public void Construct_Game_ShouldCreateGame()
        {
            Monopoly game = new Monopoly(
                new List<Player>() { }
                );
        }
        [Fact]
        public void Setup_GameWith2Players_ShouldWork()
        {
            Player horse = new Player("Horse");
            Player car = new Player("Car");

            Monopoly game = new Monopoly(
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
            List<Player> players = new List<Player>() {};

            for (int i = 0; i < numPlayers; i++)
                players.Add(new Player(i.ToString()));

            Monopoly game = new Monopoly(players);

            Assert.Throws<InvalidOperationException>(() => game.Play());
        }

        [Fact]
        public void Setup_GameWith2Players_ShouldRandomizeOrder()
        {
            bool horseFirst = false;
            bool carFirst = false;
            Player horse = new Player("Horse");
            Player car = new Player("Car");

            for (int i = 0; i < 100; i++)
            {
                Monopoly game = new Monopoly(
                    new List<Player> { horse, car }
                    );
                if (game.Players[0].Name == "Horse") horseFirst = true;
                if (game.Players[0].Name == "Car") carFirst = true;
            }

            Assert.True(horseFirst && carFirst);
        }
    }
}
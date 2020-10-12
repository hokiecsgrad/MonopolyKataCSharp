using MonopolyKata;
using MonopolyKata.Spaces;
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
            game = new Monopoly(null, null, null);
            Assert.IsType<Monopoly>(game);
        }

        [Fact]
        public void Setup_GameWith2Players_ShouldWork()
        {
            game = new Monopoly(null, null, null);
            game.AddPlayer(horse);
            game.AddPlayer(car);

            Assert.Contains(horse, game.Players);
            Assert.Contains(car, game.Players);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(9)]
        [InlineData(15)]
        public void Setup_GameWithWrongNumberOfPlayers_ShouldThrowException(int numPlayers)
        {
            game = new Monopoly(null, null, null);

            for (int i = 0; i < numPlayers; i++)
                game.AddPlayer(new Player(i.ToString()));

            Assert.Throws<InvalidOperationException>(() => game.Start());
        }

        [Fact]
        public void Setup_GameWith2Players_ShouldRandomizeOrder()
        {
            bool horseFirst = false;
            bool carFirst = false;

            for (int i = 0; i < 100; i++)
            {
                game = new Monopoly(null, null, null);
                game.AddPlayer(horse);
                game.AddPlayer(car);
                game.Start();
                if (game.Players[0].Name == "Horse") horseFirst = true;
                if (game.Players[0].Name == "Car") carFirst = true;
            }

            Assert.True(horseFirst && carFirst);
        }

        public class EmptyBoard : Board
        {
            public EmptyBoard()
            {
                AddSpace(new Empty());
            }
        }

        [Fact]
        public void Rounds_GameWith2Players_ShouldPlayFor20Rounds()
        {
            game = new Monopoly(null, null, null);
            game.AddPlayer(horse);
            game.AddPlayer(car);
            game.Start();

            Assert.Equal(20, game.Rounds);
            Assert.Equal(20, horse.Rounds);
            Assert.Equal(20, car.Rounds);
        }

        [Fact(Skip = "I don't know how to test this")]
        public void Rounds_GameWith2Players_ShouldMaintainInitialOrderForAllRounds()
        {
            game = new Monopoly(null, null, null);
            game.AddPlayer(horse);
            game.AddPlayer(car);
            game.Start();
        }
    }
}
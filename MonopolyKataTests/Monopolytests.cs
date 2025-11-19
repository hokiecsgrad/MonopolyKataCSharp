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
            game = new Monopoly(null, null, null);
        }

        [Fact]
        public void Construct_Game_ShouldCreateGame()
        {
            Assert.IsType<Monopoly>(game);
        }

        [Fact]
        public void Setup_GameWith2Players_ShouldWork()
        {
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
        public void Rounds_GameWith2Players_ShouldPlayFor200Rounds()
        {
            game.AddPlayer(horse);
            game.AddPlayer(car);
            game.Start();

            Assert.Equal(200, game.Rounds);
            Assert.Equal(200, horse.Rounds);
            Assert.Equal(200, car.Rounds);
        }
    }
}
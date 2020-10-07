using MonopolyKata;
using MonopolyKata.Spaces;
using System;
using Xunit;

namespace MonopolyKataTests
{
    class MockPlayerBoard : Board
    {
        public MockPlayerBoard()
        {
            AddSpace(new Go());
            for (int i = 1; i < 40; i++)
            {
                switch (i)
                {
                    case 5: AddSpace(new Railroad("Reading Railroad", 0, 0, new PropertyGroup("fake")));
                        break;
                    default:
                        AddSpace(new Empty());
                        break;
                }
            }
        }
    }

    public class PlayerTests
    {
        [Fact]
        public void Construct_NormalPlayer_ShouldCreateThatPlayer()
        {
            Player horse = new Player("Horse");
            Assert.IsType<Player>(horse);
        }

        [Fact]
        public void Move_PlayerToNamedSpaceInFrontOfPlayer_ShouldRepositionPlayer()
        {
            Board board = new MockPlayerBoard();
            Player horse = new Player("Horse");

            board.AddPlayerToBoard(horse, 2);
            horse.MoveToSpaceNamed("Reading Railroad");

            Assert.Equal(0, horse.Bank);
            Assert.Equal(5, horse.Position);
        }
        
        [Fact]
        public void Move_PlayerToNamedSpaceBehindPlayer_ShouldRepositionPlayer()
        {
            Board board = new MockPlayerBoard();
            Player horse = new Player("Horse");

            board.AddPlayerToBoard(horse, 38);
            horse.MoveToSpaceNamed("Reading Railroad");

            Assert.Equal(200, horse.Bank);
            Assert.Equal(5, horse.Position);
        }
        
    }
}
